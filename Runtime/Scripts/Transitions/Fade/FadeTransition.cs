using Kaynir.SceneExtension.Loaders;
using Kaynir.SceneExtension.Tools;
using System;
using System.Collections;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    [Serializable]
    public abstract class FadeTransition : Transition
    {
        [SerializeField]
        private FadeConfig fadeConfig;

        public override IEnumerator FadeInRoutine(ISceneLoader sceneLoader)
        {
            yield return FadeRoutine(fadeConfig.FadeInCurve,
                                     fadeConfig.FadeInDuration,
                                     fadeConfig.DeltaTime);
        }

        public override IEnumerator FadeOutRoutine(ISceneLoader sceneLoader)
        {
            yield return FadeRoutine(fadeConfig.FadeOutCurve,
                                     fadeConfig.FadeOutDuration,
                                     fadeConfig.DeltaTime);
        }

        public abstract void SetAlpha(float alpha);

        private IEnumerator FadeRoutine(AnimationCurve curve, float seconds, float deltaTime)
        {
            for (float t = 0f; t < seconds; t += deltaTime)
            {
                SetAlpha(curve.Evaluate(t / seconds));
                yield return null;
            }

            SetAlpha(curve.Evaluate(1f));
        }
    }
}
