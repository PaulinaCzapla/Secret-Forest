using UnityEngine;

namespace PlayerInteractions.Input
{
    public interface IInputManager
    {
        void BeginTouch(Vector2 position, bool isUI);

        // Tap & Swipe
        void Tap(Vector2 position, int pointerId, bool isUI);
        void Swipe(Vector2 beginPosition, bool isUI);

        // Press
        void BeginPress(Vector2 position, int pointerId, bool isUI);
        void Press(Vector2 position, int pointerId, bool isUI);

        // Drag
        void DragBegin(Vector2 position, bool isUI);
        void DragEnd(Vector2 position);
        void Drag(Vector2 currentPosition, Vector2 deltaPosition, bool isUI);

        // Pinch
        void PinchBegin();
        void PinchEnd();
        void Pinch(Vector2 position, Vector2 deltaPosition, float deltaMagnitudeDiff);

        // Mouse Wheel
        void MouseWheel(float value);
    }
}