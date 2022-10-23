using System;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class InputSettings
    {
        [SerializeField]public float maxTapDuration = 0.125f;
        [SerializeField]public float tapDistanceThreshold = 0.1f;

        [SerializeField] public float swipeMaxDuration = 0.3f;
        [SerializeField] public float swipeDistanseThreshold = 0.15f;
        
        [SerializeField] public float dragDurationThreshold = 0.002f;
        [SerializeField] public float dragDistanceThreashold = 0.04f;
        
       // [SerializeField] public LayerMask gladeLayerMask;
        public float DragDurationThreshold { get { return dragDurationThreshold; } }
        public float DragDistanceThreashold { get { return dragDistanceThreashold; } }
        public float MAXTapDuration { get { return maxTapDuration; } }
        public float TapDistanceThreshold { get { return tapDistanceThreshold; } }
        public float SwipeMaxDuration { get { return swipeMaxDuration; } }
        public float SwipeDistanseThreshold { get { return swipeDistanseThreshold; } }
       // public LayerMask GladeLayerMask { get { return gladeLayerMask; } }
    }
}