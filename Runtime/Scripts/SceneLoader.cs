using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kaynir.SceneExtension.Loading;
using Kaynir.SceneExtension.Tools;
using Kaynir.SceneExtension.Transitions;
using UnityEngine;

namespace Kaynir.SceneExtension
{
    public class SceneLoader : MonoBehaviour
    {
        public static event Action LoadingStarted;
        public static event Action<float> LoadingProgressed;
        public static event Action LoadingCompleted;

        [Header("Required Settings:")]
        [SerializeField] private LoadingScreen loadingScreen = null;

        [Header("Optional Settings:")]
        [SerializeField] private Transition transition = null;

        private bool isLoading;

        public void LoadSingle(int scene, params int[] extraScenes)
        {
            var operations = extraScenes.Prepend(scene).Select((buildIndex, i) =>
            {
                return SceneOperation.LoadAdditive(buildIndex, i == 0);
            });

            LoadOperations(operations, false);
        }

        public void LoadAdditive(IEnumerable<int> scenes, bool setActive, params int[] unloadScenes)
        {
            var operations = unloadScenes.Concat(scenes).Select((buildIndex, i) =>
            {
                return SceneOperation.LoadAdditive(buildIndex, setActive && i == 0);
            });

            LoadOperations(operations, true);
        }

        public void LoadAdditive(int scene, bool setActive, params int[] unloadScenes)
        {
            LoadAdditive(Enumerable.Repeat(scene, 1), setActive, unloadScenes);
        }

        private void LoadOperations(IEnumerable<SceneOperation> operations, bool isAdditive)
        {
            if (isLoading)
            {
                Debug.Log("<color=red>Loading is already in progress.</color>");
                return;
            }

            isLoading = true;
            LoadingStarted?.Invoke();
            StartCoroutine(LoadingRoutine(operations, isAdditive));
        }

        private IEnumerator LoadingRoutine(IEnumerable<SceneOperation> operations, bool isAdditive)
        {
            yield return transition?.EnterRoutine();
            yield return loadingScreen.LoadRoutine(isAdditive);
            yield return null;

            var asyncList = CreateAsyncList(operations);

            yield return LoadingProgressRoutine(asyncList);
            yield return SceneActivationRoutine(asyncList);
            
            yield return null;
            yield return loadingScreen.UnloadRoutine();
            yield return transition?.ExitRoutine();

            isLoading = false;
            LoadingCompleted?.Invoke();
        }

        private List<AsyncOperation> CreateAsyncList(IEnumerable<SceneOperation> operations)
        {
            return operations.Where(op => op.asyncFunc != null).Select(op =>
            {
                AsyncOperation asyncOperation = op.asyncFunc();
                asyncOperation.allowSceneActivation = false;
                return asyncOperation;
            }).ToList();
        }

        private IEnumerator LoadingProgressRoutine(List<AsyncOperation> asyncList)
        {
            float totalProgress = SceneConsts.MIN_PROGRESS;

            while (totalProgress < SceneConsts.MAX_PROGRESS)
            {
                totalProgress = asyncList.Average(op => op.progress);
                totalProgress /= SceneConsts.MAX_ASYNC_PROGRESS;
                totalProgress = Mathf.Clamp01(totalProgress);
                LoadingProgressed?.Invoke(totalProgress);
                yield return null;
            }
        }

        private IEnumerator SceneActivationRoutine(List<AsyncOperation> asyncList)
        {
            foreach (AsyncOperation op in asyncList)
            {
                op.allowSceneActivation = true;
                yield return null;
            }
        }
    }
}