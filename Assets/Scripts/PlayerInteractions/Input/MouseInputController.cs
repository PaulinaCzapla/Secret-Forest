using Settings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerInteractions.Input
{
    /// <summary>
    /// A class that represents an input controller for a mouse.
    /// </summary>
    public class MouseInputController : IInputController
    {
        private IInputManager _inputManager;
        private InputSettings _settings;
        private Vector2 _tapPosition;
        private Vector2 _beginTapPosition;
        private Vector2 _prevTapPosition;

        private float _timer = 0f;

        // State machine
        private delegate void StateDelegate();
        private delegate void StateOnEnterDelegate(Touch touch);

        private StateDelegate _currentState;

        public MouseInputController(IInputManager inputManager, InputSettings settings)
        {
            _settings = settings;
            _inputManager = inputManager;
            _currentState = IdleState;
        }

        public void Update()
        {
            _timer += Time.deltaTime;
            _currentState();
            _inputManager.MouseWheel(UnityEngine.Input.mouseScrollDelta.y);
        }

        private void IdleState()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                _currentState = TapDragRecognitionOnEnter;
        }

        private void TapDragRecognitionOnEnter()
        {
            _beginTapPosition = UnityEngine.Input.mousePosition;
            _timer = 0f;
            _currentState = TapDragRecognition;
        }

        private void TapDragRecognition()
        {
            // This is a tap/click
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _inputManager.Tap(UnityEngine.Input.mousePosition, -1);
                _currentState = IdleState;
            }
            else if (_timer >= _settings.dragDurationThreshold &&
                     (Mathf.Abs(Vector2.Distance(_beginTapPosition, UnityEngine.Input.mousePosition)) >=
                      _settings.dragDistanceThreashold))
            {
                DraggingStateOnEnter();
            }
        }

        private void DraggingStateOnEnter()
        {
            _prevTapPosition = UnityEngine.Input.mousePosition;
            _inputManager.DragBegin(UnityEngine.Input.mousePosition);
            _currentState = DraggingState;
        }

        private void DraggingState()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                _inputManager.Drag(UnityEngine.Input.mousePosition,
                    (Vector2) UnityEngine.Input.mousePosition - _prevTapPosition);
                _prevTapPosition = UnityEngine.Input.mousePosition;
            }
            else
            {
                _inputManager.DragEnd(UnityEngine.Input.mousePosition);
                _currentState = IdleState;
            }
        }
    }
}