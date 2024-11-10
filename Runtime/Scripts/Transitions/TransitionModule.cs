using Kaynir.SceneExtension.Loaders;
using System;
using System.Collections;

namespace Kaynir.SceneExtension.Transitions
{
    [Serializable]
    public abstract class TransitionModule : ITransitionModule
    {
        public abstract void Initialize(ISceneLoader sceneLoader);
        public abstract void Clear(ISceneLoader sceneLoader);

        public abstract IEnumerator FadeInRoutine(ISceneLoader sceneLoader);
        public abstract IEnumerator FadeOutRoutine(ISceneLoader sceneLoader);
    }
}
