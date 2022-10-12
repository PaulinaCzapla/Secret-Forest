using Settings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerInteractions.Input
{
    public class MouseInputController : IInputController
    {
        private IInputManager _inputManager;
        private InputSettings _settings;
        private Vector2 _tapPosition;
        private bool _tapPositionHasUI;
        private Vector2 _beginTapPosition;
        private Vector2 _prevTapPosition;
        private float _timer = 0f;
        // State machine
        private  delegate void StateDelegate();
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
            _currentState();
        }

        void IdleState()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                _currentState = TapDragRecognitionOnEnter;
        }

        void TapDragRecognitionOnEnter()
        {
            _beginTapPosition = UnityEngine.Input.mousePosition;
            _tapPositionHasUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(-1);
            _timer = 0f;
            _currentState = TapDragRecognition;
        }

        void TapDragRecognition()
        {
            // This is a tap/click
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _inputManager.Tap(UnityEngine.Input.mousePosition, -1, _tapPositionHasUI);
                _currentState = IdleState;
            }
        }

        void DraggingStateOnEnter()
        {
            _prevTapPosition = UnityEngine.Input.mousePosition;
            _inputManager.DragBegin(UnityEngine.Input.mousePosition, _tapPositionHasUI);
            _currentState = DraggingState;
        }

        void DraggingState()
        {
            if (UnityEngine.Input.GetMouseButton(0))
            {
                _inputManager.Drag(UnityEngine.Input.mousePosition,
                    (Vector2) UnityEngine.Input.mousePosition - _prevTapPosition, _tapPositionHasUI);
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