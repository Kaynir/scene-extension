using System.Collections;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    public class CompositeTransition : Transition
    {
        [SerializeField] private Transition[] transitions = null;

        public override IEnumerator EnterRoutine()
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                yield return transitions[i].EnterRoutine();
            }
        }

        public override IEnumerator ExitRoutine()
        {
            for (int i = transitions.Length - 1; i >= 0 ; i--)
            {
                yield return transitions[i].ExitRoutine();
            }
        }
    }
}