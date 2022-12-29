using System;
using System.Collections.Generic;
using GameManager;
using Glades;
using Glades.GladeTypes;
using PlayerInteractions.Input;
using PlayerInteractions.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerInteractions
{
    /// <summary>
    /// A class that implements player's interaction with the world.
    /// </summary>
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;

        private RaycastHit2D[] _hits;
        private bool _canInteract;
        private bool _canMove;

        private Camera MainCamera
        {
            get
            {
                if (_mainCamera == null)
                    _mainCamera = Camera.main;

                return _mainCamera;
            }
        }

        private Camera _mainCamera;

        private void OnEnable()
        {
            InputManager.onTapAction += OnSingleTap;
        }

        private void OnDisable()
        {
            InputManager.onTapAction -= OnSingleTap;
        }
        
        /// <summary>
        /// Called when player did a single tap.
        /// </summary>
        /// <param name="tapPosition"> A position of the tap. </param>
        private void OnSingleTap(Vector2 tapPosition)
        {
            if (MainCamera == null)
                return;

            CheckRaycastedObject(tapPosition, MainCamera);
        }

        /// <summary>
        /// Checks if player tapped on UI.
        /// </summary>
        /// <param name="screenPoint"> Position on the screen. </param>
        /// <returns> Returns true if tap was on UI, otherwise returns false. </returns>
        private bool IsTapOnUI(Vector2 screenPoint)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(screenPoint.x, screenPoint.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        /// <summary>
        /// Checks if the player tapped on interactable object and performs proper action.
        /// </summary>
        /// <param name="screenPoint"> A point on the screen. </param>
        /// <param name="raycastCamera"> Current camera. </param>
        private void CheckRaycastedObject(Vector2 screenPoint, Camera raycastCamera)
        {
            _hits = Physics2D.RaycastAll(raycastCamera.ScreenToWorldPoint(screenPoint), Vector3.forward, 1000f,
                layerMask);

            if (IsTapOnUI(screenPoint))
                return;
            
            foreach (var hit in _hits)
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    //If its something else than glade
                    if (!hit.transform.GetComponent<SpawnedGlade>())
                    {
                        if (hit.transform.root.TryGetComponent(out SpawnedGlade hitOnGlade))
                            if (!hitOnGlade.Id.Equals(GameManager.GameController.GetInstance().CurrentGladeID))
                                continue;
                            else
                            {
                                interactable.Interact();
                                return;
                            }
                    }

                    //if glade
                    if (hit.transform.root.TryGetComponent(out SpawnedGlade glade))
                        if (!glade.Id.Equals(GameManager.GameController.GetInstance().CurrentGladeID))
                        {
                            interactable.Interact();
                            return;
                        }
                }
            }
        }
    }
}