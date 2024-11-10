using Kaynir.SceneExtension.Loaders;
using System.Collections;

namespace Kaynir.SceneExtension.Transitions
{
    public abstract class ProgressModule : TransitionModule
    {
        public override void Initialize(ISceneLoader sceneLoader)
        {
            SetProgress(0f);
            sceneLoader.LoadTicked += OnLoadTicked;
        }

        public override void Clear(ISceneLoader sceneLoader)
        {
            sceneLoader.LoadTicked -= OnLoadTicked;
        }

        public override IEnumerator FadeInRoutine(ISceneLoader sceneLoader)
        {
            yield break;
        }

        public override IEnumerator FadeOutRoutine(ISceneLoader sceneLoader)
        {
            yield break;
        }

        protected abstract void SetProgress(float progress);

        private void OnLoadTicked(int sceneBuildIndex, float progress)
        {
            SetProgress(progress);
        }
    }
}
