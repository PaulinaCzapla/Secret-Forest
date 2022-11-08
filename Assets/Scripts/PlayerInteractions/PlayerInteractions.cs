using System;
using GameManager;
using Glades;
using Glades.GladeTypes;
using PlayerInteractions.Input;
using PlayerInteractions.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerInteractions
{
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

        void OnSingleTap(Vector2 tapPosition, bool isUI)
        {
            if (MainCamera == null)
                return;

            CheckRaycastedObject(tapPosition, MainCamera);
        }

        void CheckRaycastedObject(Vector2 screenPoint, Camera raycastCamera)
        {
            _hits = Physics2D.RaycastAll(raycastCamera.ScreenToWorldPoint(screenPoint), Vector3.forward, 1000f,
                layerMask);
            foreach (var hit in _hits)
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    //If its something else than glade
                    if (!hit.transform.GetComponent<SpawnedGlade>())
                    {
                        if (hit.transform.root.TryGetComponent(out SpawnedGlade hitOnGlade))
                            if (!hitOnGlade.Id.Equals(GameManager.GameManager.GetInstance().CurrentGladeID))
                                continue;
                            else
                            {
                                interactable.Interact();
                                return;
                            }
                    }

                    //if glade
                    if (hit.transform.root.TryGetComponent(out SpawnedGlade glade))
                        if (!glade.Id.Equals(GameManager.GameManager.GetInstance().CurrentGladeID))
                        {
                            interactable.Interact();
                            return;
                        }
                }
            }
        }
    }
}