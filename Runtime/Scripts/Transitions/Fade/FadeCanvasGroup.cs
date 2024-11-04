using Kaynir.SceneExtension.Tools;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    public class FadeCanvasGroup : FadeSource
    {
        private readonly Canvas _canvas;
        private readonly CanvasGroup _canvasGroup;

        public FadeCanvasGroup(Canvas canvas, CanvasGroup canvasGroup)
        {
            _canvas = canvas;
            _canvasGroup = canvasGroup;
        }

        public override void SetAlpha(float alpha)
        {
            _canvas.enabled = alpha > 0f;
            _canvasGroup.alpha = alpha;
        }
    }
}