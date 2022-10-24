using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;


namespace Effects
{
    public class FireLightEffect : MonoBehaviour
    {
        [SerializeField] private Light2D light;
        [Range(0,100)]
        [SerializeField] private float brightnessMultiplier;
        [Range(0,100)]
        [SerializeField] private float radiusMultiplier;
        [Range(1,200)]
        [Tooltip("The higher it is, the slower the light changes")]
        [SerializeField] private float speedDivider;
        [Header("Light intensity change curve")]
        [SerializeField] private AnimationCurve lightBrightness;
        
        private float _time = 0;
        private float _initialIntensity, _initialOuterR, _initialInnerR;
        private void Awake()
        {
            _time = Random.Range(0f, 0.9f);
            _initialIntensity = light.intensity;
            _initialInnerR = light.pointLightInnerRadius;
            _initialOuterR = light.pointLightOuterRadius;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            _time /= speedDivider;
            
            light.intensity = _initialIntensity + lightBrightness.Evaluate(_time) * brightnessMultiplier;
            light.pointLightInnerRadius = _initialInnerR + lightBrightness.Evaluate(_time) * radiusMultiplier;
            light.pointLightOuterRadius = _initialOuterR + lightBrightness.Evaluate(_time) * radiusMultiplier;
            
            if (_time >= 1)
                _time = 0;
        }
    }
}