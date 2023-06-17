using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace UI
{
    /// <summary>
    /// It adjusts a size of RectTransform, so it fits various kinds of displays, including those with notches.
    /// </summary>
    public class SafeAreaFitter : MonoBehaviour
    {
        private RectTransform _panel;
        private Rect _lastSafeArea = new Rect(0, 0, 0, 0);

        private void Awake()
        {
            _panel = GetComponent<RectTransform>();
            Refresh();
        }

        private void Update()
        {
            Refresh();
        }

        private void Refresh()
        {
            Rect safeArea = GetSafeArea();

            if (safeArea != _lastSafeArea)
            {
                ApplySafeArea(safeArea);
            }
        }

        private Rect GetSafeArea()
        {
            return Screen.safeArea;
        }
        /// <summary>
        /// Applies safe area size to the RectTransform component.
        /// </summary>
        private void ApplySafeArea(Rect r)
        {
            _lastSafeArea = r;

            Vector2 anchorMin = r.position;
            Vector2 anchorMax = r.position + r.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            _panel.anchorMin = anchorMin;
            _panel.anchorMax = anchorMax;
        }
    }
}