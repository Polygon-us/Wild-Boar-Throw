using UnityEngine.UI;
using UnityEngine;
using UI.Generic;
using System;
using Utils;

namespace Gameplay.Controllers
{
    public class GameEndController : MonoBehaviour
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private RectTransform banner;
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button leaderboardBtn;
        [SerializeField] private Button mainMenuBtn;

        [SerializeField] private TweenParams tweenParams;
        private Image _backgroundImage;
        private CanvasScaler _canvasScaler;
        private float UpPosition => _canvasScaler.referenceResolution.y;

        public Action OnRestart;
        public Action OnLeaderboard;
        public Action OnMainMenu;

        private void Awake()
        {
            _backgroundImage = panel.GetComponent<Image>();
            _canvasScaler = panel.GetComponentInParent<CanvasScaler>();

            restartBtn.onClick.AddListener(() => OnRestart?.Invoke());
            leaderboardBtn.onClick.AddListener(() => OnLeaderboard?.Invoke());
            mainMenuBtn.onClick.AddListener(() => OnMainMenu?.Invoke());

            panel.gameObject.SetActive(false);
        }

        public void Open()
        {
            panel.gameObject.SetActive(true);
            banner.anchoredPosition = new Vector2(banner.anchoredPosition.x, UpPosition);

            LeanTween.value(0f, 0.5f, tweenParams.duration)
                .setEase(tweenParams.inType)
                .setOnUpdate(_backgroundImage.SetAlpha);
            LeanTween.moveY(banner, 0, tweenParams.duration)
                .setEase(tweenParams.inType);
        }

        public void Close()
        {
            LeanTween.value(0.5f, 0.0f, tweenParams.duration)
                .setEase(tweenParams.outType)
                .setOnUpdate(_backgroundImage.SetAlpha);

            LeanTween.moveY(banner, UpPosition, tweenParams.duration)
                .setEase(tweenParams.outType)
                .setOnComplete(() => { panel.gameObject.SetActive(false); });
        }
    }
}