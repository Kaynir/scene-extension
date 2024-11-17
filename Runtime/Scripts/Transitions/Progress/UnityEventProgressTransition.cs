using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kaynir.SceneExtension.Transitions
{
    [Serializable]
    public class UnityEventProgressTransition : ProgressTransition
    {
        [SerializeField]
        private UnityEvent startProgress = new();

        [SerializeField]
        private UnityEvent<float> updateProgress = new();

        [SerializeField]
        private UnityEvent endProgress = new();

        protected override void SetProgress(float progress)
        {
            switch (progress)
            {
                case 0f: startProgress.Invoke(); break;
                case 1f: endProgress.Invoke(); break;
                default: updateProgress.Invoke(progress); break;
            }
        }
    }
}
