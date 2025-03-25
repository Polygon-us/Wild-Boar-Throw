using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace Gameplay.Controllers
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private RectTransform tutorialPanel;
        [SerializeField] private RectTransform banner;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Button closeBtn;
        [SerializeField] private Button xBtn;
        [SerializeField] private float tweenTime = 0.5f;
        [SerializeField] private LeanTweenType tweenType = LeanTweenType.easeOutCubic;

        private Action OnClose;

        private Image _backgroundImage;
        private CanvasScaler _canvasScaler;

        private float UpPosition => _canvasScaler.referenceResolution.y;
        
        private void Awake()
        {
            _backgroundImage = tutorialPanel.GetComponent<Image>();
            _canvasScaler = tutorialPanel.GetComponentInParent<CanvasScaler>();
            closeBtn.onClick.AddListener(Close);
            xBtn.onClick.AddListener(Close);
            Internal_Close();
        }

        public void Open(string text, Action onClose)
        {
            tutorialPanel.gameObject.SetActive(true);
            messageText.text = text;
            OnClose = onClose;

            banner.anchoredPosition = new Vector2(banner.anchoredPosition.x, UpPosition);

            LeanTween.value(0f, 0.5f, tweenTime)
                .setEase(tweenType)
                .setOnUpdate(SetColor);
            LeanTween.moveY(banner, 0, tweenTime)
                .setEase(tweenType);
        }

        private void SetColor(float value)
        {
            Color c = _backgroundImage.color;
            c.a = value;
            _backgroundImage.color = c;
        }

        private void Close()
        {
            LeanTween.value(0.5f, 0.0f, tweenTime)
                .setEase(tweenType)
                .setOnUpdate(SetColor);

            LeanTween.moveY(banner, UpPosition, tweenTime)
                .setEase(tweenType)
                .setOnComplete(() =>
                {
                    tutorialPanel.gameObject.SetActive(false); 
                    OnClose?.Invoke();
                });
        }

        private void Internal_Close()
        {
            tutorialPanel.gameObject.SetActive(false);
            OnClose = null;
        }
    }
}