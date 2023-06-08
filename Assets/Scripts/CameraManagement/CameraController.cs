using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using CombatSystem;
using DG.Tweening;
using Glades;
using InteractableItems.CollectableItems.Items;
using LevelGenerating;
using PlayerInteractions.Input;
using PlayerInteractions.StaticEvents;
using UI.Eq;
using UnityEngine;

namespace CameraManagement
{
    /// <summary>
    ///  A static class that contains and calculates camera limits.
    /// </summary>
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
            Debug.Log(MinX + "     " + MaxX);
            Debug.Log(MinY + "     " + MaxY);
        }
    }

    /// <summary>
    ///  A class that is responsible for camera movement.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cam;
        [SerializeField] private CinemachineVirtualCamera zoomCam;
        [SerializeField] private GameObject player;
        [SerializeField] private float initialCameraZoom;
        
        private readonly float pinchFactor = 0.01f;
        private bool _isDragging;
        private bool _isPinching;
        private Vector2 _prevPos;

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
            StaticCombatEvents.SubscribeToCombatStarted(EnableZoomCamera);
            StaticCombatEvents.SubscribeToCombatEnded(DisableZoomCamera);
            GladesStaticEvents.SubscribeToUnlockGlades(UnlockGlades);
            PlayerStatsStaticEvents.SubscribeToPlayerDied(DisableZoomCamera);
            LevelGenerator.OnLevelGenerated += DisableZoomCamera;
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
            StaticCombatEvents.UnsubscribeFromCombatStarted(EnableZoomCamera);
            StaticCombatEvents.UnsubscribeFromCombatEnded(DisableZoomCamera);
            GladesStaticEvents.UnsubscribeFromUnlockGlades(UnlockGlades);
            PlayerStatsStaticEvents.UnsubscribeFromPlayerDied(DisableZoomCamera);
            LevelGenerator.OnLevelGenerated -= DisableZoomCamera;
        }

        private void UnlockGlades(List<SpawnedGlade> glades)
        {
            StartCoroutine(ShowGlades(glades));
        }

        /// <summary>
        ///  Moves the camera to the glades positions given as a parameter.
        /// </summary>
        /// <param name="glades"> A list of glades to show on camera. </param>
        private IEnumerator ShowGlades(List<SpawnedGlade> glades)
        {
            Inventory.Instance.CloseStorage();
            zoomCam.gameObject.SetActive(true);
            cam.gameObject.SetActive(false);
            foreach (var glade in glades)
            {
                zoomCam.Follow = glade.transform;
                yield return new WaitForSeconds(1.5f);
            }

            DisableZoomCamera();
        }

        private void OnPinch(Vector2 position, Vector2 delta, float magnitude)
        {
            Zoom(magnitude * pinchFactor);
        }
        
        /// <summary>
        ///  Disables the camera that can be zoomed by user.
        /// </summary>
        private void DisableZoomCamera()
        {
            cam.m_Lens.OrthographicSize = initialCameraZoom;
            cam.Follow = player.transform;
            cam.gameObject.SetActive(true);
            zoomCam.gameObject.SetActive(false);
        }

        /// <summary>
        ///  Enables the camera that can be zoomed by user.
        /// </summary>
        private void EnableZoomCamera(Enemy enemy)
        {
            zoomCam.Follow = GameManager.GameController.GetInstance().CurrentGlade.transform;
            zoomCam.gameObject.SetActive(true);
            cam.gameObject.SetActive(false);
        }

        private void OnPinchBegin()
        {
            _isPinching = true;
        }

        private void OnPinchEnd()
        {
            _isPinching = false;
        }

        /// <summary>
        ///  Moves the camera according to the finger movement.
        /// </summary>
        /// <param name="currentPosition"> Current finger position on screen. </param>
        /// <param name="deltaPosition"> Delta position of finger movement. </param>
        private void OnDragAction(Vector2 currentPosition, Vector2 deltaPosition)
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

        /// <summary>
        ///  Zooms the camera.
        /// </summary>
        /// <param name="magnitude"> Zoom's magnitude. </param>

        void Zoom(float magnitude)
        {
            float potentialOrthoSize = Mathf.Clamp(cam.m_Lens.OrthographicSize + magnitude * 2, CameraLimits.MinZoom,
                CameraLimits.MaxZoom);
            if (cam.m_Lens.OrthographicSize != potentialOrthoSize)
            {
                cam.m_Lens.OrthographicSize = potentialOrthoSize;
            }
        }

        /// <summary>
        ///  Called when a new level is loaded. Calculates new camera limits and focuses camera on a Player.
        /// </summary>
        private void OnLevelLoaded()
        {
            CameraLimits.CalculateLimits();
            FocusPlayer();
        }

        /// <summary>
        ///  Makes the camera focus te player and sets initial zoom value.
        /// </summary>
        public void FocusPlayer()
        {
            cam.m_Lens.OrthographicSize = initialCameraZoom;
            cam.Follow = player.transform;
        }
    }
}