using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kaynir.SceneExtension.Tools;
using Kaynir.SceneExtension.Transitions;
using UnityEngine;

namespace Kaynir.SceneExtension.Loaders
{
    public class SceneLoader : ISceneLoader
    {
        public event ISceneLoader.SceneAction LoadStartEvent;
        public event ISceneLoader.SceneAction LoadProgressEvent;
        public event ISceneLoader.SceneAction LoadEndEvent;

        private List<AsyncOperation> _operations;

        public SceneLoader()
        {
            _operations = new();
        }

        public void LoadScene(ISceneTransition transition, CancellationToken token = default, params int[] buildIndexes)
        {
            if (_operations.Any(operation => !operation.isDone))
            {
                Debug.Log("<color=red>Loading is already in progress.</color>");
                return;
            }

            if (buildIndexes.Length == 0)
                return;

            _operations.Clear();
            LoadSceneTask(transition, token, buildIndexes).Forget();
        }

        public void LoadScene(int buildIndex, ISceneTransition transition, CancellationToken token = default)
        {
            LoadScene(transition, token, buildIndex);
        }

        private async UniTaskVoid LoadSceneTask(ISceneTransition transition, CancellationToken token, int[] buildIndexes)
        {
            LoadStartEvent?.Invoke(buildIndexes[0], SceneConsts.MIN_PROGRESS);

            await transition.FadeInTask(token);
            await LoadOperationsTask(buildIndexes, token);
            await transition.FadeOutTask(token);

            LoadEndEvent?.Invoke(buildIndexes[0], SceneConsts.MAX_PROGRESS);
        }

        private async UniTask LoadOperationsTask(int[] buildIndexes, CancellationToken token)
        {
            _operations = SceneHelper.GetLoadOperations(buildIndexes);

            float totalProgress = SceneConsts.MIN_PROGRESS;

            while (totalProgress < SceneConsts.MAX_PROGRESS)
            {
                totalProgress = _operations.Average(operation => operation.progress);
                totalProgress = Mathf.Clamp01(totalProgress / SceneConsts.MAX_ASYNC_PROGRESS);
                LoadProgressEvent?.Invoke(buildIndexes[0], totalProgress);
                await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, token);
            }

            _operations.ForEach(operation => operation.allowSceneActivation = true);
        }
    }
}
