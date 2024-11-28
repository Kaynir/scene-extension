using Kaynir.SceneExtension.Loaders;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Kaynir.SceneExtension.Transitions
{
    public class UnityEventTransition : Transition
    {
        [SerializeField]
        private UnityEvent<ISceneLoader> fadeInEvent = new();

        [SerializeField]
        private UnityEvent<ISceneLoader> fadeOutEvent = new();

        public override void Initialize(ISceneLoader sceneLoader) { }
        public override void Clear(ISceneLoader sceneLoader) { }

        public override IEnumerator FadeInRoutine(ISceneLoader sceneLoader)
        {
            fadeInEvent.Invoke(sceneLoader);
            yield break;
        }

        public override IEnumerator FadeOutRoutine(ISceneLoader sceneLoader)
        {
            fadeOutEvent.Invoke(sceneLoader);
            yield break;
        }
    }
}
