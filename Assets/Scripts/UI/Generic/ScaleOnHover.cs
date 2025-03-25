using UnityEngine.EventSystems;
using UnityEngine;

namespace UI.Generic
{
    public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float scale = 1.2f;
        [SerializeField] private LeanTweenType leanTweenType = LeanTweenType.easeOutCubic;
        [SerializeField] private float duration = 0.3f;

        private int _tweenId;
        
        private RectTransform RectTransform => (RectTransform)transform;

        public void OnPointerEnter(PointerEventData eventData)
        {
            LeanTween.cancel(_tweenId);
            _tweenId = LeanTween.scale(RectTransform, Vector3.one * scale, duration)
                .setEase(leanTweenType)
                .uniqueId;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            LeanTween.cancel(_tweenId);
            _tweenId = LeanTween.scale(RectTransform, Vector3.one, duration)
                .setEase(leanTweenType)
                .uniqueId;
        }
    }
}