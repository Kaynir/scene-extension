using Kaynir.SceneExtension.Loaders;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Kaynir.SceneExtension.Transitions
{
    public class UnityEventModule : TransitionModule
    {
        [SerializeField]
        private UnityEvent<ISceneLoader> initializeEvent = new();

        [SerializeField]
        private UnityEvent<ISceneLoader> clearEvent = new();

        public override void Initialize(ISceneLoader sceneLoader)
        {
            initializeEvent.Invoke(sceneLoader);
        }

        public override void Clear(ISceneLoader sceneLoader)
        {
            clearEvent.Invoke(sceneLoader);
        }

        public override IEnumerator FadeInRoutine(ISceneLoader sceneLoader)
        {
            yield break;
        }

        public override IEnumerator FadeOutRoutine(ISceneLoader sceneLoader)
        {
            yield break;
        }
    }
}
