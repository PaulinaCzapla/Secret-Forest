using Settings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerInteractions.Input
{
    public class TouchInputController : IInputController
    {
        private readonly IInputManager _inputManager;
        private readonly InputSettings _settings;
        private float _deltaMagnitudeDiff;
        private float _touchOneTimer = 0f;
        private Vector2 _beginTouchPosition;
        private bool _thisTouchIsOnUi = false;
        //state machine
        private delegate void StateDelegate(int touchCount);
        private delegate void StateOnEnterDelegate(Touch touch);
        private StateDelegate _currentState;

        public TouchInputController(IInputManager inputManager, InputSettings settings)
        {
            _inputManager = inputManager;
            _settings = settings;
            _currentState = IdleState;
        }

        public void Update()
        {
            // use one call to get touchCount for optimisation
            int touchCount = UnityEngine.Input.touchCount;
            // any state update
            AnyState(touchCount);
            // update current state
            _currentState(touchCount);
        }

        void AnyState(int touchCount)
        {
            if (touchCount == 2 && _currentState != PinchState)
            {
                ChangeState(PinchState, UnityEngine.Input.GetTouch(0), PinchStateOnEnter);
            }
        }

        void PinchStateOnEnter(Touch touch)
        {
            _inputManager.PinchBegin();
            _inputManager.BeginTouch(
                Midpoint(UnityEngine.Input.GetTouch(0).position, UnityEngine.Input.GetTouch(1).position)
                , _thisTouchIsOnUi);
        }

        void PinchState(int touchCount)
        {
            if (touchCount < 2)
            {
                _inputManager.PinchEnd();
                ChangeState(IdleState);
                return;
            }

            Touch touchZero = UnityEngine.Input.GetTouch(0);
            Touch touchOne = UnityEngine.Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            _deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            _inputManager.Pinch(
                Vector3.Lerp(touchOne.position, touchZero.position, 0.5f),
                Vector3.Lerp(touchOne.position, touchZero.position, 0.5f) -
                Vector3.Lerp(touchOnePrevPos, touchZeroPrevPos, 0.5f),
                _deltaMagnitudeDiff
            );

            _inputManager.Drag(
                Midpoint(touchZero.position, touchOne.position),
                Midpoint(touchZero.deltaPosition, touchOne.deltaPosition),
                _thisTouchIsOnUi
            );
        }

        void IdleState(int touchCount)
        {
            if (touchCount == 1)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    // for touch IsPointerOverGameObject is working only at touch.phase == TouchPhase.Began
                    _thisTouchIsOnUi = EventSystem.current != null &&
                                       EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                    // invoke Begin Touch method
                    _inputManager.BeginTouch(touch.position, _thisTouchIsOnUi);

                    //  if (CameraManager.IsRaycastADynamicCube(touch.position))
                    {
                        // change state to Tap Swipe Drag Recognition State
                        ChangeState(TapSwipeDragRecognitionState, touch, TapSwipeDragRecognitionOnEnter);
                    }
                    //  else
                    {
                        // change state to Tap Drag Recognition State
                     //   ChangeState(TapDragRecognitionState, touch, TapSwipeDragRecognitionOnEnter);
                    }
                }
            }
        }

        void TapSwipeDragRecognitionOnEnter(Touch touch)
        {
            //Debug.Log("==Tap/Swipe/Drag State");
            _touchOneTimer = 0f;
            _beginTouchPosition = touch.position;
        }

        // void TapDragRecognitionState(int touchCount)
        // {
        //     Touch touch = UnityEngine.Input.GetTouch(0);
        //
        //     _touchOneTimer += Time.unscaledDeltaTime;
        //
        //     if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        //     {
        //         // Check if timer is greater than drag duration
        //         if (CheckForDragDuration(_touchOneTimer, touch, _settings.DragDurationThreshold))
        //             return;
        //     }
        //     else if (touch.phase == TouchPhase.Ended)
        //     {
        //         float physicalDistance = PhysicalDistance(_beginTouchPosition, touch.position);
        //         // Tap Recognition
        //         if (!CheckForTap(_touchOneTimer, physicalDistance, touch.fingerId))
        //         {
        //             // Swipe Recognition if Tap recognision failed
        //             CheckForSwipe(_touchOneTimer, physicalDistance);
        //         }
        //
        //         ChangeState(IdleState);
        //     }
        // }

        void TapSwipeDragRecognitionState(int touchCount)
        {
            Touch touch = UnityEngine.Input.GetTouch(0);

            _touchOneTimer += Time.unscaledDeltaTime;

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                // If timer is greater that swipe max duration
              //  if (_touchOneTimer > _settings.SwipeMaxDuration)
                {
                    // Check if timer is greater than drag duration
                    if (CheckForDragDuration(_touchOneTimer, touch, _settings.DragDurationThreshold))
                        return;

                    // Check if distance is greater than drag distance threashold
                    if (CheckForDragDistance(touch, _settings.DragDistanceThreashold))
                        return;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float physicalDistance = PhysicalDistance(_beginTouchPosition, touch.position);

                // Tap Recognition
                if (!CheckForTap(_touchOneTimer, physicalDistance, touch.fingerId))
                {
                    // Swipe Recognition if Tap recognision failed
                    CheckForSwipe(_touchOneTimer, physicalDistance);
                }

                ChangeState(IdleState);
            }
        }

        void DraggingStateOnEnter(Touch touch)
        {
            _inputManager.DragBegin(touch.position, _thisTouchIsOnUi);
        }

        void DraggingState(int touchCount)
        {
            Touch touch = UnityEngine.Input.GetTouch(0);
            _inputManager.Drag(touch.position, touch.deltaPosition, _thisTouchIsOnUi);

            if (touch.phase == TouchPhase.Ended)
            {
                _inputManager.DragEnd(touch.position);
                ChangeState(IdleState);
            }
        }

        bool CheckForTap(float timer, float distance, int fingerId)
        {
            if (timer < _settings.MAXTapDuration && distance < _settings.TapDistanceThreshold)
            {
                _inputManager.Tap(_beginTouchPosition, fingerId, _thisTouchIsOnUi);
                return true;
            }

            return false;
        }

        bool CheckForSwipe(float timer, float distance)
        {
            if (timer < _settings.SwipeMaxDuration && distance > _settings.SwipeDistanseThreshold)
            {
                _inputManager.Swipe(_beginTouchPosition, _thisTouchIsOnUi);
                return true;
            }

            return false;
        }

        bool CheckForDragDuration(float timer, Touch touch, float durationThreshold)
        {
            if (timer > durationThreshold)
            {
                ChangeState(DraggingState, touch, DraggingStateOnEnter);
                return true;
            }

            return false;
        }

        bool CheckForDragDistance(Touch touch, float distanceThreashold)
        {
            if (PhysicalDistance(_beginTouchPosition, touch.position) > distanceThreashold)
            {
                ChangeState(DraggingState, touch, DraggingStateOnEnter);
                return true;
            }

            return false;
        }

        float PhysicalDistance(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b) / Screen.dpi /** 0.0254f*/;
        }

        Vector2 Midpoint(Vector2 a, Vector2 b)
        {
            return new Vector2((a.x + b.x) * 0.5f, (a.y + b.y) * 0.5f);
        }

        void ChangeState(StateDelegate state)
        {
            _currentState = state;
        }

        void ChangeState(StateDelegate state, Touch touch, StateOnEnterDelegate stateOnEnter = null)
        {
            if (stateOnEnter != null)
                stateOnEnter(touch);

            _currentState = state;
        }
    }
}