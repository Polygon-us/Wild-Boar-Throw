using UnityEngine;
using TMPro;

namespace Gameplay.Controllers
{
    public class CountdownController : MonoBehaviour
    {
        [SerializeField] private RectTransform countdownPanel;
        [SerializeField] private TMP_Text actionText;
        [SerializeField] private TMP_Text countText;

        private int _countdownTime;
        
        private void Start()
        {
            Close();
        }

        public void Open(int time, string text)
        {
            _countdownTime = time;
            countText.text = time.ToString();
            actionText.text = text;
            countdownPanel.gameObject.SetActive(true);
        }

        public void Close()
        {
            countdownPanel.gameObject.SetActive(false);
        }

        public void StartCountDown()
        {
            LeanTween.value(_countdownTime, 0, _countdownTime)
                .setOnUpdate(value => countText.text = Mathf.CeilToInt(value).ToString());

            countText.rectTransform.localScale = Vector3.one;
            LeanTween.scale(countText.rectTransform, Vector3.one * 1.2f, 1)
                .setRepeat(_countdownTime);
        }
    }
}