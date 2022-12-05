using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Utilities;

namespace Effects
{
    [RequireComponent(typeof(Light2D))]
    public class FireplaceLight : MonoBehaviour
    {
        [SerializeField] private float intensityOffset;
        [SerializeField] private float minIntensityDuration, maxIntensityDuration;
        [SerializeField] private int intensitySteps;
        [SerializeField] private Ease intensityEase;

        [Space(10)]
        [SerializeField] private float radiusOffset;
        [SerializeField] private float minRadiusDuration, maxRadiusDuration;
        [SerializeField] private int radiusSteps;
        [SerializeField] private Ease radiusEase;

        private Light2D light2D;
        private Sequence intenstitySequence, innerRadiusSequence, outerRadiusSequence;

        private void Awake() 
        {
            light2D = GetComponent<Light2D>();    
        }

        private void Start() 
        {
            var beginIntensity = light2D.intensity;
            var beginInnerRadius = light2D.pointLightInnerRadius;
            var beginOuterRadius = light2D.pointLightOuterRadius;

            intenstitySequence = DOTween.Sequence()
                .SetAutoKill(false)
                .SetLoops(-1, LoopType.Restart);
            var intensityPoints = RandomizePoints(-intensityOffset, 0, intensitySteps);
            for(int i = 0; i < intensityPoints.Count; ++i)
            {
                intenstitySequence.Append(light2D.DOIntensity(beginIntensity + intensityPoints[i], Random.Range(minIntensityDuration, maxIntensityDuration))
                    .SetEase(intensityEase));
            }
     
            innerRadiusSequence = DOTween.Sequence()
                .SetAutoKill(false)
                .SetLoops(-1, LoopType.Restart);
            outerRadiusSequence = DOTween.Sequence()
                .SetAutoKill(false)
                .SetLoops(-1, LoopType.Restart);
            var radiusPoints = RandomizePoints(-radiusOffset, radiusOffset, radiusSteps);
            for(int i = 0; i < radiusPoints.Count; ++i)
            {
                var duration = Random.Range(minRadiusDuration, maxRadiusDuration); 
                innerRadiusSequence.Append(light2D.DOInnerRadius(beginInnerRadius + radiusPoints[i], duration)
                    .SetEase(radiusEase));
                outerRadiusSequence.Append(light2D.DOOuterRadius(beginOuterRadius + radiusPoints[i], duration)
                    .SetEase(radiusEase));
            }
        }

        private List<float> RandomizePoints(float a, float b, float n)
        {
            List<float> ret = new List<float>();
            for(int i = 0; i < n; ++i)
            {
                ret.Add(Random.Range(a, b));
            }

            return ret;
        }

        private void Destroy() 
        {
            intenstitySequence.Kill();
            outerRadiusSequence.Kill();
            innerRadiusSequence.Kill();
        }
    }
}
