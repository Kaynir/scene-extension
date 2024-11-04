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
        private readonly MonoBehaviour _parentBehaviour;

        public SceneLoader(MonoBehaviour parentBehaviour)
        {
            _sceneOperations = new();
            _parentBehaviour = parentBehaviour;
        }

        public void LoadScene(int sceneBuildIndex, ISceneTransition sceneTransition, Action onBeforeExit = null)
        {
            LoadScenes(Enumerable.Empty<int>().Append(sceneBuildIndex), sceneTransition, onBeforeExit);
        }

        public void LoadScenes(IEnumerable<int> sceneBuildIndexes, ISceneTransition sceneTransition, Action onBeforeExit = null)
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

            _parentBehaviour.StartCoroutine(LoadRoutine(sceneBuildIndexes, sceneTransition, onBeforeExit));
        }

        public void ReloadActiveScene(ISceneTransition sceneTransition, Action onLoadEnded = null)
        {
            LoadScene(SceneHelper.GetActiveSceneBuildIndex(), sceneTransition, onLoadEnded);
        }

        private IEnumerator LoadRoutine(IEnumerable<int> sceneBuildIndexes, ISceneTransition sceneTransition, Action onBeforeExit)
        {
            LoadStarted?.Invoke(sceneBuildIndexes.ElementAt(0), 0f);

            //await transition.FadeInTask(token);
            yield return LoadOperationRoutine(sceneBuildIndexes, onBeforeExit);
            //await transition.FadeOutTask(token);

            LoadEnded?.Invoke(sceneBuildIndexes.ElementAt(0), 1f);
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

            onBeforeExit?.Invoke();
            _sceneOperations.ForEach(operation => operation.allowSceneActivation = true);
            _sceneOperations.Clear();
        }
    }
}
