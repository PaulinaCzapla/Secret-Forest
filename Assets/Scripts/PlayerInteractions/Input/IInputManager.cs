using UnityEngine;

namespace PlayerInteractions.Input
{
    public interface IInputManager
    {
        void BeginTouch(Vector2 position);

        // Tap & Swipe
        void Tap(Vector2 position, int pointerId);
        void Swipe(Vector2 beginPosition);

        // Press
        void BeginPress(Vector2 position, int pointerId);
        void Press(Vector2 position, int pointerId);

        // Drag
        void DragBegin(Vector2 position);
        void DragEnd(Vector2 position);
        void Drag(Vector2 currentPosition, Vector2 deltaPosition);

        // Pinch
        void PinchBegin();
        void PinchEnd();
        void Pinch(Vector2 position, Vector2 deltaPosition, float deltaMagnitudeDiff);

        // Mouse Wheel
        void MouseWheel(float value);
    }
}