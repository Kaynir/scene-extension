using Kaynir.SceneExtension.Loaders;
using UnityEngine;
using UnityEngine.UI;

namespace Kaynir.SceneExtension.Transitions
{
    public class ImageProgressTransition : ProgressTransition
    {
        [SerializeField]
        private Image filledImage;

        public override void Initialize(ISceneLoader sceneLoader)
        {
            base.Initialize(sceneLoader);
            filledImage.enabled = true;
        }

        public override void Clear(ISceneLoader sceneLoader)
        {
            base.Clear(sceneLoader);
            filledImage.enabled = false;
        }

        protected override void SetProgress(float progress)
        {
            filledImage.fillAmount = progress;
        }
    }
}
