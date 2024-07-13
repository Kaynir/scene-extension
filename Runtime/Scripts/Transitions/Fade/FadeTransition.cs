using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions.Fade
{
    public class FadeTransition : SceneTransition
    {
        private readonly FadeSource _source;
        private readonly BlendingCurve _fadeInCurve;
        private readonly BlendingCurve _fadeOutCurve;

        public FadeTransition(FadeSource source, BlendingCurve fadeInCurve, BlendingCurve fadeOutCurve)
        {
            _source = source;
            _fadeInCurve = fadeInCurve;
            _fadeOutCurve = fadeOutCurve;
        }

        public override async UniTask FadeInTask(CancellationToken cancellationToken = default)
        {
            await FadeTask(_fadeInCurve, cancellationToken);
        }

        public override async UniTask FadeOutTask(CancellationToken cancellationToken = default)
        {
            await FadeTask(_fadeOutCurve, cancellationToken);
        }

        private async UniTask FadeTask(BlendingCurve blendingCurve, CancellationToken cancellationToken = default)
        {
            for (float t = 0f; t < blendingCurve.Seconds; t += Time.deltaTime)
            {
                _source.SetAlpha(blendingCurve.Evaluate(t));
                await UniTask.Yield(PlayerLoopTiming.LastPostLateUpdate, cancellationToken);
            }

            _source.SetAlpha(blendingCurve.EvaluateMax());
        }
    }
}
