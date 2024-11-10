using Kaynir.SceneExtension.Loaders;
using System;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    [Serializable]
    public class CanvasFadeModule : FadeModule
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private CanvasGroup canvasGroup;

        public override void Initialize(ISceneLoader sceneLoader)
        {
            canvas.enabled = true;
            SetAlpha(0f);
        }

        public override void Clear(ISceneLoader sceneLoader)
        {
            canvas.enabled = false;
        }

        public override void SetAlpha(float alpha)
        {
            canvasGroup.alpha = alpha;
        }
    }
}
