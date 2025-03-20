using UnityEngine;
using System;
using TMPro;

namespace Gameplay.Controllers
{
    public class CountdownController : MonoBehaviour
    {
        [SerializeField] private int countdownTime = 3;
        [SerializeField] private RectTransform countdownPanel;
        [SerializeField] private TMP_Text actionText;
        [SerializeField] private TMP_Text countText;

        private int _countdownTween;
        private int _scaleTween;
        
        private void Start()
        {
            Close();
        }

        public void Open(string text)
        {
            countText.text = countdownTime.ToString();
            actionText.text = text;
            countdownPanel.gameObject.SetActive(true);
        }

        public void Close()
        {
            countdownPanel.gameObject.SetActive(false);
        }

        public void StartCountDown(Action callback)
        { 
            _countdownTween = LeanTween.value(countdownTime, 0, countdownTime)
                .setOnUpdate(value => countText.text = Mathf.CeilToInt(value).ToString()).uniqueId;

            countText.rectTransform.localScale = Vector3.one;
            _scaleTween = LeanTween.scale(countText.rectTransform, Vector3.one * 1.2f, 1)
                .setRepeat(countdownTime)
                .setOnComplete(callback).uniqueId;
        }

        public void StopCountDown()
        {
            LeanTween.cancel(_countdownTween);
            LeanTween.cancel(_scaleTween);
        }

        public void Reset()
        {
            StopCountDown();
            countText.rectTransform.localScale = Vector3.one;
        }
    }
}