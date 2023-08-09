using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    public abstract class FadeSource : MonoBehaviour
    {
        public abstract void SetAlpha(float alpha);
    }
}