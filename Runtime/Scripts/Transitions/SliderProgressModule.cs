using Kaynir.SceneExtension.Loaders;
using UnityEngine;
using UnityEngine.UI;

namespace Kaynir.SceneExtension.Transitions
{
    public class SliderProgressModule : ProgressModule
    {
        [SerializeField]
        private Slider slider;

        public override void Initialize(ISceneLoader sceneLoader)
        {
            base.Initialize(sceneLoader);
            slider.enabled = true;
        }

        public override void Clear(ISceneLoader sceneLoader)
        {
            base.Clear(sceneLoader);
            slider.enabled = false;
        }

        protected override void SetProgress(float progress)
        {
            slider.value = Mathf.Lerp(slider.minValue, slider.maxValue, progress);
        }
    }
}
