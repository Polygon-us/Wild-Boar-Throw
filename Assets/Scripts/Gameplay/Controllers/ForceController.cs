using UnityEngine.UI;
using UnityEngine;
using UI.Generic;
using TMPro;

namespace Gameplay.Controllers
{
    public class ForceController : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float maxForce = 100f;
        [SerializeField, Range(0, 1)] private float incrementPercentage = 0.1f;
        [SerializeField, Range(0, 1)] private float decrementPercentage = 0.1f;
        [SerializeField] private AnimationCurve chargeCurve;
        [Header("UI")]
        [SerializeField] private RectTransform panel;
        [SerializeField] private Button clickBtn;
        [SerializeField] private TMP_Text stateText;
        [SerializeField] private TweenParams tweenParams;
        
        public float MaxForce => maxForce;
        public float IncrementPercentage => incrementPercentage;
        public float DecrementPercentage => decrementPercentage;

        public AnimationCurve ChargeCurve => chargeCurve;

        public Button ClickBtn => clickBtn;

        public TMP_Text StateText => stateText;

        private int _tweenId;
        private Vector3 _startPos;

        private void Awake()
        {
            _startPos = panel.anchoredPosition;
            Hide(true);
        }   

        public void Show()
        {
            _tweenId = LeanTween.move(panel, _startPos, tweenParams.duration)
                .setEase(tweenParams.inType)
                .uniqueId;
        }

        public void Hide(bool instant = false)
        {
            _tweenId = LeanTween.move(panel, -(Vector3)panel.sizeDelta - _startPos, instant ? 0 : tweenParams.duration)
                .setEase(tweenParams.outType)
                .uniqueId;
        }
    }
}