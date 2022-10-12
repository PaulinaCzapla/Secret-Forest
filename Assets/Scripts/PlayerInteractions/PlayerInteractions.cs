using System;
using PlayerInteractions.Input;
using PlayerInteractions.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerInteractions
{
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;

        // private variables
        private RaycastHit2D _hit;

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
            // if (isUI)
            //  return;

            if (MainCamera == null)
                return;

            CheckRaycastedObject(tapPosition, MainCamera);
        }

        void CheckRaycastedObject(Vector2 screenPoint, Camera raycastCamera)
        {
            // Debug.DrawLine();
            _hit = Physics2D.Raycast(raycastCamera.ScreenToWorldPoint(screenPoint), Vector2.up, 1000f, layerMask);
            if (_hit)
            {
                IInteractable interactable = _hit.transform.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}