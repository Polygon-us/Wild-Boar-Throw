using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

namespace Gameplay.Controllers
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Button closeBtn;

        private Action OnClose;
        
        private void Awake()
        {
            closeBtn.onClick.AddListener(Close);
            Close();
        }

        public void Open(string text, Action onClose)
        {
            tutorialPanel.SetActive(true);
            messageText.text = text;
            OnClose = onClose;
        }

        private void Close()
        {
            tutorialPanel.SetActive(false);
            OnClose?.Invoke();
        }
    }
}