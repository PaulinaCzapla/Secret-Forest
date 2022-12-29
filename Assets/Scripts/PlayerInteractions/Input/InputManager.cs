using System;
using Settings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerInteractions.Input
{
    /// <summary>
    /// Describes input type.
    /// </summary>
    public enum InputType
    {
        Touch,
        Mouse
    };
    /// <summary>
    /// A class that is responsible for input management.
    /// </summary>
    public class InputManager : MonoBehaviour, IInputManager
    {
        public static Action<Vector2> onTouchBegin; //<position, isUI>
        public static Action<Vector2> onTapAction; //<position, isUI>
        public static Action<Vector2> onPressBegin; //<position, isUI>
        public static Action<Vector2> onPressAction; //<position, isUI>
        public static Action<Vector2, Vector2, float> onPinchAction; //<positon, deltaPosition, magnitude>
        public static Action<float> onMouseWheelAction;
        public static Action<Vector2, Vector2> onDragAction; //<position, deltaPosition, isUI>
        public static Action<Vector2> onDragBegin;
        public static Action<Vector2> onDragEnd;
        public static Action onPinchBegin;
        public static Action onPinchEnd;
        public static Action<Vector2> onSwipe; //begin swipe position


        /// <summary>
        /// Gets or sets a value indicating whether tap enable.
        /// </summary>
        /// <value>true if tap enable; otherwise, false.</value>
        public static bool TapEnable { get; set; }

        [SerializeField] InputType inputTouch = InputType.Touch;
        [SerializeField] private InputSettings settings;

        private IInputController _InputController;

        private void Awake()
        {
            TapEnable = true;
            
 #if UNITY_ANDROID && !UNITY_EDITOR
             inputTouch = InputType.Touch;
 #else
            inputTouch = InputType.Mouse;
#endif

            switch (inputTouch)
            {
                case InputType.Mouse:
                    _InputController = new MouseInputController(this, settings);
                    break;
                case InputType.Touch:
                    _InputController = new TouchInputController(this, settings);
                    break;
            }
        }

        public void BeginTouch(Vector2 position)
        {
            if (onTouchBegin != null)
                onTouchBegin(position);
        }

        public void Swipe(Vector2 begitPosition)
        {
            if (onSwipe != null)
                onSwipe(begitPosition);
        }

        public void MouseWheel(float value)
        {
            if (onMouseWheelAction != null)
                onMouseWheelAction(value);
        }

        public void BeginPress(Vector2 tapPosition, int pointerId)
        {
            if (!TapEnable)
                return;

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(pointerId))
            {
                if (onPressBegin != null)
                {
                    onPressBegin(tapPosition);
                    return;
                }
            }

            if (onPressBegin != null)
                onPressBegin(tapPosition);
        }

        public void Press(Vector2 tapPosition, int pointerId)
        {
            if (!TapEnable)
                return;

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(pointerId))
            {
                if (onPressAction != null)
                {
                    onPressAction(tapPosition);
                    return;
                }
            }

            if (onPressAction != null)
                onPressAction(tapPosition);
        }
        

        public void Tap(Vector2 tapPosition, int pointerId)
        {
            if (!TapEnable)
                return;
            
            Debug.Log(EventSystem.current.IsPointerOverGameObject(pointerId));
            if (EventSystem.current != null )
            {
                if (onTapAction != null)
                {
                    onTapAction?.Invoke(tapPosition);
                    return;
                }
            }

            if (onTapAction != null)
                onTapAction?.Invoke(tapPosition);
        }

        bool m_dragBeinOnUI = false;

        public void DragBegin(Vector2 position)
        {
            if (onDragBegin != null) onDragBegin(position);
        }

        public void DragEnd(Vector2 position)
        {
            if (m_dragBeinOnUI)
                return;
            
            if (onDragEnd != null) onDragEnd(position);
        }

        public void Drag(Vector2 currentPosition, Vector2 deltaPosition)
        {
            if (m_dragBeinOnUI)
                return;

            if (onDragAction != null)
            {
                onDragAction(currentPosition, deltaPosition);
            }
        }

        public void PinchBegin()
        {
            if (onPinchBegin != null) onPinchBegin();
        }

        public void PinchEnd()
        {
            if (onPinchEnd != null) onPinchEnd();
        }

        public void Pinch(Vector2 position, Vector2 deltaPosition, float deltaMagnitudeDiff)
        {
            if (onPinchAction != null)
            {
                onPinchAction(position, deltaPosition, deltaMagnitudeDiff);
            }
        }

        void Update()
        {
            _InputController.Update();
        }
    }
}