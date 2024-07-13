using System;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions.Fade
{
    [Serializable]
    public struct BlendingCurve
    {
        public AnimationCurve Curve;
        public float Seconds;

        public float Evaluate(float seconds)
        {
            return Curve.Evaluate(seconds / Seconds);
        }

        public float EvaluateMax() => Evaluate(Seconds);
    }
}
