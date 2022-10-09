using UnityEngine;

namespace RandomGenerators
{
    public class ValueRandomizer : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve;
        
        public float CurveWeightedRandom() 
        {
            return curve.Evaluate(Random.value);
        }
    }
}