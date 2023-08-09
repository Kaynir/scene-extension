using Kaynir.SceneExtension.Tools;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    public class FadeCanvasGroup : FadeSource
    {
        [SerializeField] private Canvas canvas = null;
        [SerializeField] private CanvasGroup canvasGroup = null;

        public override void SetAlpha(float alpha)
        {
            canvas.enabled = alpha > SceneConsts.MIN_PROGRESS;
            canvasGroup.alpha = alpha;
        }
    }
}