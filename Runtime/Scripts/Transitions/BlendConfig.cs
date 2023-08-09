using Kaynir.SceneExtension.Tools;
using UnityEngine;

namespace Kaynir.SceneExtension.Transitions
{
    [CreateAssetMenu(menuName = AssetMenuTools.MAIN_PATH + "Blend Config")]
    public class BlendConfig : ScriptableObject
    {
        [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        [SerializeField, Min(0f)] private float seconds = .5f;

        public AnimationCurve Curve => curve;
        public float Seconds => seconds;

        public float Evaluate(float timeInSeconds)
        {
            return curve.Evaluate(timeInSeconds / seconds);
        }

        public float EvaluateMax() => Evaluate(Seconds);
    }
}