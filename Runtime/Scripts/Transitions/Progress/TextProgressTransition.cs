using Kaynir.SceneExtension.Loaders;
using TMPro;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    public class TextProgressTransition : ProgressTransition
    {
        [SerializeField]
        private TMP_Text textField;

        [SerializeField]
        private string textFormat = "{0}%";

        [SerializeField]
        private float valueMultiplier = 100f;

        public override void Initialize(ISceneLoader sceneLoader)
        {
            base.Initialize(sceneLoader);
            textField.enabled = true;
        }

        public override void Clear(ISceneLoader sceneLoader)
        {
            base.Clear(sceneLoader);
            textField.enabled = false;
        }

        protected override void SetProgress(float progress)
        {
            textField.SetText(textFormat, progress * valueMultiplier);
        }
    }
}
