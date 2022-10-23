using System;
using Settings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerInteractions.Input
{
    public enum InputType
    {
        Touch,
        Mouse
    };

    public class InputManager : MonoBehaviour, IInputManager
    {
        public static Action<Vector2, bool> onTouchBegin; //<position, isUI>
        public static Action<Vector2, bool> onTapAction; //<position, isUI>
        public static Action<Vector2, bool> onPressBegin; //<position, isUI>
        public static Action<Vector2, bool> onPressAction; //<position, isUI>
        public static Action<Vector2, Vector2, float> onPinchAction; //<positon, deltaPosition, magnitude>
        public static Action<float> onMouseWheelAction;
        public static Action<Vector2, Vector2, bool> onDragAction; //<position, deltaPosition, isUI>
        public static Action<Vector2> onDragBegin;
        public static Action<Vector2> onDragEnd;
        public static Action onPinchBegin;
        public static Action onPinchEnd;
        public static Action<Vector2> onSwipe; //begin swipe position


        /// <summary>
        /// Gets or sets a value indicating whether tap enable.
        /// </summary>
        /// <value><c>true</c> if tap enable; otherwise, <c>false</c>.</value>
        public static bool tapEnable { get; set; }

        [SerializeField] InputType inputTouch = InputType.Touch;
        [SerializeField] private InputSettings settings;

        private IInputController _InputController;

        private void Awake()
        {
            tapEnable = true;


// #if MOBILE_VER && !UNITY_EDITOR
//             m_InputTouch = InputType.Touch;
// #elif MOUSE_VER && !UNITY_EDITOR
    //        m_InputTouch = InputType.Mouse;
//#endif

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

#if UNITY_EDITOR
        public void TapDebugAction(Vector2 tapPosition, int pointerId)
        {
            Tap(tapPosition, pointerId, false);
        }
#endif

        public void BeginTouch(Vector2 position, bool isUI)
        {
            if (onTouchBegin != null)
                onTouchBegin(position, isUI);
        }

        public void Swipe(Vector2 begitPosition, bool isUI)
        {
            if (isUI)
                return;

            if (onSwipe != null)
                onSwipe(begitPosition);
        }

        public void MouseWheel(float value)
        {
            if (onMouseWheelAction != null)
                onMouseWheelAction(value);
        }

        public void BeginPress(Vector2 tapPosition, int pointerId, bool isUI)
        {
            if (!tapEnable)
                return;

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(pointerId))
            {
                if (onPressBegin != null)
                {
                    onPressBegin(tapPosition, true);
                    return;
                }
            }

            if (onPressBegin != null)
                onPressBegin(tapPosition, isUI);
        }

        public void Press(Vector2 tapPosition, int pointerId, bool isUI)
        {
#if INPUT_DEBUG_MANAGER
            Debug.LogFormat("HoldTap : {0}", tapPosition);
#endif

            if (!tapEnable)
                return;

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(pointerId))
            {
#if INPUT_DEBUG_MANAGER
                Debug.LogFormat("HoldTap the UI with pointerId : {0}", pointerId);
#endif
                if (onPressAction != null)
                {
                    onPressAction(tapPosition, true);
                    return;
                }
            }

            if (onPressAction != null)
                onPressAction(tapPosition, isUI);
        }

        public void Tap(Vector2 tapPosition, int pointerId, bool isUI)
        {
#if INPUT_DEBUG_MANAGER
            Debug.LogFormat ("Tap : {0}", tapPosition);
#endif

            if (!tapEnable)
                return;

            //EventSystem.current.isP
            Debug.Log(EventSystem.current.IsPointerOverGameObject(pointerId));
            if (EventSystem.current != null )
            {
#if INPUT_DEBUG_MANAGER
                Debug.LogFormat("Tapped the UI with pointerId : {0}", pointerId);
#endif
                if (onTapAction != null)
                {
                    onTapAction?.Invoke(tapPosition, true);
                    return;
                }
            }

            if (onTapAction != null)
                onTapAction?.Invoke(tapPosition, isUI);
        }

        bool m_dragBeinOnUI = false;

        public void DragBegin(Vector2 position, bool isUI)
        {
            m_dragBeinOnUI = isUI;

            if (m_dragBeinOnUI)
                return;

#if INPUT_DEBUG_MANAGER
            Debug.Log ("Drag Begin");
#endif
            if (onDragBegin != null) onDragBegin(position);
        }

        public void DragEnd(Vector2 position)
        {
            if (m_dragBeinOnUI)
                return;

#if INPUT_DEBUG_MANAGER
            Debug.Log ("Drag End");
#endif
            if (onDragEnd != null) onDragEnd(position);
        }

        public void Drag(Vector2 currentPosition, Vector2 deltaPosition, bool isUI)
        {
            if (m_dragBeinOnUI)
                return;

            if (onDragAction != null)
            {
                onDragAction(currentPosition, deltaPosition, isUI);
            }
        }

        public void PinchBegin()
        {
#if INPUT_DEBUG_MANAGER
            Debug.Log ("Pinch Begin");
#endif
            if (onPinchBegin != null) onPinchBegin();
        }

        public void PinchEnd()
        {
#if INPUT_DEBUG_MANAGER
            Debug.Log ("Pinch End");
#endif
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