using System;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    [Serializable]
    public class WaitInputKeyTransition : WaitTransition
    {
        [SerializeField]
        private bool isAnyKey = false;

        [SerializeField]
        private KeyCode keyCode = KeyCode.None;

        protected override bool IsAwaitingActivation()
        {
            if (isAnyKey && Input.anyKeyDown)
                return false;

            if (Input.GetKeyDown(keyCode))
                return false;

            return true;
        }
    }
}
