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

        private Light2D _light2D;
        private Sequence _intenstitySequence, _innerRadiusSequence, _outerRadiusSequence;

        private void Awake() 
        {
            _light2D = GetComponent<Light2D>();    
        }

        private void Start() 
        {
            var beginIntensity = _light2D.intensity;
            var beginInnerRadius = _light2D.pointLightInnerRadius;
            var beginOuterRadius = _light2D.pointLightOuterRadius;

            _intenstitySequence = DOTween.Sequence()
                .SetAutoKill(false)
                .SetLoops(-1, LoopType.Restart);
            var intensityPoints = RandomizePoints(-intensityOffset, 0, intensitySteps);
            for(int i = 0; i < intensityPoints.Count; ++i)
            {
                _intenstitySequence.Append(_light2D.DOIntensity(beginIntensity + intensityPoints[i], Random.Range(minIntensityDuration, maxIntensityDuration))
                    .SetEase(intensityEase));
            }
     
            _innerRadiusSequence = DOTween.Sequence()
                .SetAutoKill(false)
                .SetLoops(-1, LoopType.Restart);
            _outerRadiusSequence = DOTween.Sequence()
                .SetAutoKill(false)
                .SetLoops(-1, LoopType.Restart);
            var radiusPoints = RandomizePoints(-radiusOffset, radiusOffset, radiusSteps);
            for(int i = 0; i < radiusPoints.Count; ++i)
            {
                var duration = Random.Range(minRadiusDuration, maxRadiusDuration); 
                _innerRadiusSequence.Append(_light2D.DOInnerRadius(beginInnerRadius + radiusPoints[i], duration)
                    .SetEase(radiusEase));
                _outerRadiusSequence.Append(_light2D.DOOuterRadius(beginOuterRadius + radiusPoints[i], duration)
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
            _intenstitySequence.Kill();
            _outerRadiusSequence.Kill();
            _innerRadiusSequence.Kill();
        }
    }
}
