using System.Collections;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    public abstract class Transition : MonoBehaviour
    {
        public abstract IEnumerator EnterRoutine();
        public abstract IEnumerator ExitRoutine();
    }
}