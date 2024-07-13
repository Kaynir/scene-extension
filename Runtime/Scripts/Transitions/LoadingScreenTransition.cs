using Cysharp.Threading.Tasks;
using Kaynir.SceneExtension.Loaders;
using Kaynir.SceneExtension.Tools;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Kaynir.SceneExtension.Transitions
{
    public class LoadingScreenTransition : SceneTransition
    {
        private readonly Canvas _canvas;
        private readonly Image _progressBar;
        private readonly ISceneLoader _sceneLoader;

        public LoadingScreenTransition(Canvas canvas, Image progressBar, ISceneLoader sceneLoader)
        {
            _canvas = canvas;
            _progressBar = progressBar;
            _sceneLoader = sceneLoader;
        }

        public override UniTask FadeInTask(CancellationToken cancellationToken = default)
        {
            _canvas.enabled = true;
            _sceneLoader.LoadProgressEvent += OnProgressUpdated;
            SetProgress(SceneConsts.MIN_PROGRESS);
            return UniTask.Yield(cancellationToken);
        }

        public override UniTask FadeOutTask(CancellationToken cancellationToken = default)
        {
            _canvas.enabled = false;
            _sceneLoader.LoadProgressEvent -= OnProgressUpdated;
            SetProgress(SceneConsts.MAX_PROGRESS);
            return UniTask.Yield(cancellationToken);
        }

        private void SetProgress(float progress)
        {
            _progressBar.fillAmount = progress;
        }

        private void OnProgressUpdated(int buildIndex, float progress)
        {
            SetProgress(progress);
        }
    }
}