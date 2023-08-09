using UnityEngine;
using UnityEngine.UI;

namespace Kaynir.SceneExtension.UI
{
    public class UI_ImageProgressBar : UI_ProgressBar
    {
        [SerializeField] private Image filledImage = null;

        public override void SetProgress(float progress)
        {
            filledImage.fillAmount = progress;
        }
    }
}