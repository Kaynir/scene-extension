using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kaynir.SceneExtension.Tools;
using Kaynir.SceneExtension.Transitions;
using UnityEngine;

namespace Kaynir.SceneExtension.Loaders
{
    public class SceneLoader : ISceneLoader
    {
        public const float MAX_ASYNC_PROGRESS = .9f;

        public event ISceneLoader.SceneAction LoadStarted;
        public event ISceneLoader.SceneAction LoadTicked;
        public event ISceneLoader.SceneAction LoadEnded;

        private readonly List<AsyncOperation> _sceneOperations;
        private readonly MonoBehaviour _parent;
        private Func<bool> _waitActivation;

        public SceneLoader(MonoBehaviour persistentParent)
        {
            _sceneOperations = new();
            _parent = persistentParent;
        }

        public void LoadScene(int sceneBuildIndex, ITransition transition, Action onBeforeExit = null)
        {
            LoadScenes(Enumerable.Empty<int>().Append(sceneBuildIndex), transition, onBeforeExit);
        }

        public void LoadScenes(IEnumerable<int> sceneBuildIndexes, ITransition transition, Action onBeforeExit = null)
        {
            if (_sceneOperations.Any(operation => !operation.isDone))
            {
                Debug.Log("<color=yellow>Loading is already in progress!</color>");
                return;
            }

            if (sceneBuildIndexes.Count() == 0)
            {
                Debug.Log("<color=red>Unable to load empty scene collection!</color>");
                return;
            }

            _parent.StartCoroutine(LoadRoutine(sceneBuildIndexes, transition, onBeforeExit));
        }

        public void ReloadActiveScene(ITransition sceneTransition, Action onLoadEnded = null)
        {
            LoadScene(SceneHelper.GetActiveSceneBuildIndex(), sceneTransition, onLoadEnded);
        }

        public void SetWaitActivation(Func<bool> waitActivation)
        {
            _waitActivation = waitActivation;
        }

        private IEnumerator LoadRoutine(IEnumerable<int> sceneBuildIndexes, ITransition transition, Action onBeforeExit)
        {
            LoadStarted?.Invoke(sceneBuildIndexes.First(), 0f);

            transition.Initialize(this);
            yield return transition.FadeInRoutine(this);
            yield return LoadOperationRoutine(sceneBuildIndexes, onBeforeExit);
            yield return transition.FadeOutRoutine(this);
            transition.Clear(this);

            LoadEnded?.Invoke(sceneBuildIndexes.First(), 1f);
        }

        private IEnumerator LoadOperationRoutine(IEnumerable<int> sceneBuildIndexes, Action onBeforeExit)
        {
            SceneHelper.CreateSceneOperations(sceneBuildIndexes, _sceneOperations);

            float totalProgress = 0f;
            int firstSceneBuildIndex = sceneBuildIndexes.First();

            while (totalProgress < 1f)
            {
                totalProgress = _sceneOperations.Average(operation => operation.progress);
                totalProgress = Mathf.Clamp01(totalProgress / MAX_ASYNC_PROGRESS);
                LoadTicked?.Invoke(firstSceneBuildIndex, totalProgress);
                yield return null;
            }

            yield return WaitActivationRoutine();

            onBeforeExit?.Invoke();
            _sceneOperations.ForEach(operation => operation.allowSceneActivation = true);
            _sceneOperations.Clear();
        }

        private IEnumerator WaitActivationRoutine()
        {
            if (_waitActivation == null)
            {
                yield break;
            }

            while (_waitActivation())
            {
                yield return null;
            }
        }
    }
}
