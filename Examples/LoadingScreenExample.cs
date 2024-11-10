using Kaynir.SceneExtension.Loaders;
using Kaynir.SceneExtension.Transitions;
using UnityEngine;

namespace Kaynir.SceneExtension.Examples
{
    public class LoadingScreenExample : MonoBehaviour
    {
        [SerializeField]
        private TransitionSequence transitionSequence;

        private ISceneLoader _sceneLoader;

        private void Awake()
        {
            _sceneLoader = new SceneLoader(this);
        }

        public void LoadScene(int buildIndex)
        {
            _sceneLoader.LoadScene(buildIndex, transitionSequence);
        }
    }
}
