using UnityEngine;
using System;

namespace UI.Generic
{
    public class SafeArea : MonoBehaviour
    {
        private RectTransform rectTransform;
        private Rect safeArea;
        private Vector2 minAnchor;
        private Vector2 maxAnchor;

        private static Action<float> OnAdSafeAreaChanged;

        private void OnEnable()
        {
            OnAdSafeAreaChanged += RefreshAdSafeArea;
        }

        private void OnDisable()
        {
            OnAdSafeAreaChanged -= RefreshAdSafeArea;
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();

            CalculateAnchors(Screen.safeArea);
        }

        public static void SetAdSafeArea(float height)
        {
            OnAdSafeAreaChanged?.Invoke(height);       
        }
        
        private void CalculateAnchors(Rect _safeArea)
        {
            safeArea = _safeArea;
            minAnchor = safeArea.position;
            maxAnchor = minAnchor + safeArea.size;
            minAnchor.x /= Screen.width;
            minAnchor.y /= Screen.height;
            maxAnchor.x /= Screen.width;
            maxAnchor.y /= Screen.height;
            rectTransform.anchorMin = minAnchor;
            rectTransform.anchorMax = maxAnchor;
        }

        private void RefreshAdSafeArea(float height)
        {
            var sa = Screen.safeArea;
            sa.yMax -= height;
            CalculateAnchors(sa);
        }
    }
}