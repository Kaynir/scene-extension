using UnityEngine;

namespace Kaynir.SceneExtension.Tools
{
    [CreateAssetMenu(menuName = "Kaynir/Scene Extension/Fade Config")]
    public class FadeConfig : ScriptableObject
    {
        [SerializeField]
        private AnimationCurve fadeInCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [SerializeField]
        private float fadeInDuration = 1f;

        [SerializeField]
        private AnimationCurve fadeOutCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

        [SerializeField]
        private float fadeOutDuration = 1f;

        [SerializeField]
        private bool UseUnscaledDeltaTime = false;

        public AnimationCurve FadeInCurve => fadeInCurve;
        public AnimationCurve FadeOutCurve => fadeOutCurve;

        public float FadeInDuration => fadeInDuration;
        public float FadeOutDuration => fadeOutDuration;

        public float DeltaTime => UseUnscaledDeltaTime
            ? Time.unscaledDeltaTime
            : Time.deltaTime;
    }
}
