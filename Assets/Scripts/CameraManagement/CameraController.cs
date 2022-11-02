using Cinemachine;
using DG.Tweening;
using LevelGenerating;
using PlayerInteractions.Input;
using UnityEngine;

namespace CameraManagement
{
    public static class CameraLimits
    {
        public static float MinZoom { get; } = 1;
        public static float MaxZoom { get; } = 18;
        public static float MinX { get; set; }
        public static float MaxX { get; set; }
        public static float MinY { get; set; }
        public static float MaxY { get; set; }

        private static float offset = 3;

        public static void CalculateLimits(float offsetMultiplier = 1)
        {
            MinX -= offset * offsetMultiplier;
            MinY -= offset * offsetMultiplier;
            MaxX += offset * offsetMultiplier;
            MaxY += offset * offsetMultiplier;
            
            Debug.Log("camera limits");
            Debug.Log(MinX+ "     " + MaxX);
            Debug.Log(MinY+ "     " + MaxY);
        }
    }

    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cam;
        [SerializeField] private GameObject player;

        [Header("Camera movement settings")] [SerializeField]
        private float zoomSpeed = 1;

        [SerializeField] private float initialCameraZoom;
        private readonly float pinchFactor = 0.1f;

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
            LevelGenerator.OnLevelGenerated += OnLevelLoaded;
        }

        void OnPinch(Vector2 position, Vector2 delta, float magnitude)
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
            LevelGenerator.OnLevelGenerated -= OnLevelLoaded;
        }

        private void OnPinchBegin()
        {
            _isPinching = true;
        }

        private void OnPinchEnd()
        {
            _isPinching = false;
        }

        private Vector2 prevPos;

        private void OnDragAction(Vector2 currentPosition, Vector2 deltaPosition, bool isUI)
        {
            var prevPosition = currentPosition - deltaPosition;
            var prev = Camera.main.ScreenToWorldPoint(prevPosition);
            currentPosition = Camera.main.ScreenToWorldPoint(currentPosition);
            var dir = currentPosition - (Vector2) prev;
            
            
            var newPosition = cam.transform.position + new Vector3(-dir.x, -dir.y, 0);
            newPosition = new Vector3(Mathf.Clamp(newPosition.x, CameraLimits.MinX, CameraLimits.MaxX),
                Mathf.Clamp(newPosition.y, CameraLimits.MinY, CameraLimits.MaxY), newPosition.z);
                
            cam.transform.position = newPosition;
        }

        private void OnDragBegin(Vector2 position)
        {
            _isDragging = true;
            cam.Follow = null;
        }

        private void OnDragEnd(Vector2 position)
        {
            _isDragging = false;
        }

        private void OnMouseWheel(float magnitude)
        {
            Zoom(magnitude * pinchFactor);
        }

        void Zoom(float magnitude)
        {
            float potentialOrthoSize = Mathf.Clamp(cam.m_Lens.OrthographicSize + magnitude * 2, CameraLimits.MinZoom, CameraLimits.MaxZoom);
            if (cam.m_Lens.OrthographicSize != potentialOrthoSize)
            {
                cam.m_Lens.OrthographicSize = potentialOrthoSize;
            }
        }
        
        
        private void OnLevelLoaded()
        {
            CameraLimits.CalculateLimits();
            cam.m_Lens.OrthographicSize = initialCameraZoom;
            cam.Follow = player.transform;
        }
    }
}