using Kaynir.SceneExtension.Loaders;
using System;
using System.Collections;

namespace Kaynir.SceneExtension.Transitions
{
    [Serializable]
    public abstract class WaitTransition : Transition
    {
        public override void Initialize(ISceneLoader sceneLoader)
        {
            sceneLoader.SetWaitActivation(IsAwaitingActivation);
        }

        public override void Clear(ISceneLoader sceneLoader)
        {
            sceneLoader.SetWaitActivation(null);
        }

        public override IEnumerator FadeInRoutine(ISceneLoader sceneLoader)
        {
            yield break;
        }

        public override IEnumerator FadeOutRoutine(ISceneLoader sceneLoader)
        {
            yield break;
        }

        protected abstract bool IsAwaitingActivation();
    }
}
