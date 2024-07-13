using Kaynir.SceneExtension.Loaders;
using Kaynir.SceneExtension.Transitions;
using Kaynir.SceneExtension.Transitions.Fade;
using UnityEngine;

namespace Kaynir.SceneExtension.Examples
{
    public class LoadingScreenExample : MonoBehaviour
    {
        [SerializeField]
        private Canvas fadeCanvas;

        [SerializeField]
        private CanvasGroup fadeCanvasGroup;

        [SerializeField]
        private BlendingCurve fadeInCurve;

        [SerializeField]
        private BlendingCurve fadeOutCurve;

        private ISceneLoader _sceneLoader;
        private ISceneTransition _sceneTransition;

        private void Awake()
        {
            FadeSource fadeSource = new FadeCanvasGroup(fadeCanvas, fadeCanvasGroup);

            _sceneLoader = new SceneLoader();
            _sceneTransition = new FadeTransition(fadeSource, fadeInCurve, fadeOutCurve);
        }

        public void LoadScene(int buildIndex)
        {
            _sceneLoader.LoadScene(buildIndex, _sceneTransition, destroyCancellationToken);
        }
    }
}
