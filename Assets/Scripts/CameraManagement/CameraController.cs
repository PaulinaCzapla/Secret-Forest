using Cinemachine;
using PlayerInteractions.Input;
using UnityEngine;

namespace CameraManagement
{
    public static class CameraLimits
    {
        public static float MinX { get; set; }
        public static float MaxX { get; set; }
        public static float MinY { get; set; }
        public static float MaxY { get;  set; }

        private static float offset = 5;

        public static void CalculateLimits()
        {
            MinX -= offset;
            MinY -= offset;
            MaxX += offset;
            MaxY += offset;
        }
    }

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cam;
        [SerializeField] private GameObject player;

        [Header("Camera movement settings")] [SerializeField]
        private float zoomSpeed = 1;

        [SerializeField] private float pinchFactor = 1;

        private bool _isDragging;
        private bool _isPinching;
        private void OnEnable()
        {
            InputManager.onDragAction += OnDragAction;
            InputManager.onDragBegin += OnDragBegin;
            InputManager.onDragEnd += OnDragEnd;
            InputManager.onMouseWheelAction += OnMouseWheel;
            InputManager.onPinchAction += OnPinch;

            InputManager.onPinchBegin += OnPinchBegin;
            InputManager.onPinchEnd += OnPinchEnd;
        }

        void OnPinch (Vector2 position, Vector2 delta, float magnitude)
        {
            // if (m_StateUpdate != PanningZoomingUpdte)
            //     return;

            Zoom(magnitude * pinchFactor);
        }

        private void OnDisable()
        {
            InputManager.onDragAction -= OnDragAction;
            InputManager.onDragBegin -= OnDragBegin;
            InputManager.onDragEnd -= OnDragEnd;
            InputManager.onMouseWheelAction -= OnMouseWheel;
            InputManager.onPinchAction -= OnPinch;
            
            InputManager.onPinchBegin -= OnPinchBegin;
            InputManager.onPinchEnd -= OnPinchEnd;
        }

        private void OnPinchBegin()
        {
            _isPinching = true;
        }

        private void OnPinchEnd()
        {
            _isPinching = false;
        }

        private void OnDragAction(Vector2 currentPosition, Vector2 deltaPosition, bool isUI)
        {
            throw new System.NotImplementedException();
        }

        private void OnDragBegin(Vector2 position)
        {
            _isDragging = true;
        }

        private void OnDragEnd(Vector2 position)
        {
            _isDragging = false;
        }

        private void OnMouseWheel(float magnitude)
        {
            throw new System.NotImplementedException();
        }

        void Zoom(float magnitude)
        {
            // float potentialOrthoSize = m_OrthoSize + magnitude * m_ZoomSpeed;
            // CalculateLimits(m_Position, potentialOrthoSize);
            //
            // if (m_StateUpdate == LockUpdate)
            //     return;
            //
            // m_OrthoSize = potentialOrthoSize;
            // //Rebound();
        }
        private void LateUpdate()
        {
            // if (!m_HasInitialized)
            //     return;
            //
            // // apply hard limits
            // if (m_ApplyHardLimits)
            // {
            //     ApplyHardLimit();
            // }
            //
            // // apply zoom
            // m_PlayerCamera.orthographicSize = m_OrthoSize;
            //
            // // apply movement
            // m_PlayerCamera.transform.localPosition = m_Position;
        }
        
        private void OnLevelLoaded()
        {
            CameraLimits.CalculateLimits();
        }
    }
}