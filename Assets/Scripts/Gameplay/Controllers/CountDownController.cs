using UnityEngine;
using System;
using TMPro;

namespace Gameplay.Controllers
{
    public class CountdownController : MonoBehaviour
    {
        [SerializeField] private int count = 3;
        [SerializeField] private TMP_Text actionText;
        [SerializeField] private TMP_Text countText;

        private void Start()
        {
            Close();
        }

        public void Open(string text)
        {
            gameObject.SetActive(true);
            actionText.text = text;
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void StartCountDown(Action callback)
        {
            LeanTween.value(count, 0, count)
                .setOnUpdate(value => countText.text = Mathf.CeilToInt(value).ToString());

            countText.rectTransform.localScale = Vector3.one;
            LeanTween.scale(countText.rectTransform, Vector3.one * 1.2f, 1)
                .setRepeat(count)
                .setOnComplete(callback);
        }
    }
}