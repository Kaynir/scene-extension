using System.Collections;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    public class FadeTransition : Transition
    {
        [SerializeField] private FadeSource source = null;
        [SerializeField] private BlendConfig fadeInConfig = null;
        [SerializeField] private BlendConfig fadeOutConfig = null;

        public override IEnumerator EnterRoutine()
        {
            yield return FadeRoutine(fadeInConfig);
        }

        public override IEnumerator ExitRoutine()
        {
            yield return FadeRoutine(fadeOutConfig);
        }

        private IEnumerator FadeRoutine(BlendConfig config)
        {
            for (float t = 0f; t < config.Seconds; t += Time.deltaTime)
            {
                source.SetAlpha(config.Evaluate(t));
                yield return null;
            }

            source.SetAlpha(config.EvaluateMax());
        }
    }
}