using UnityEngine.UI;
using UnityEngine;
using System;

namespace Gameplay.Controllers
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private Button closeBtn;

        private Action OnClose;
        
        private void Awake()
        {
            closeBtn.onClick.AddListener(Close);
        }

        public void Open(Action onClose)
        {
            tutorialPanel.SetActive(true);
            OnClose = onClose;
        }

        private void Close()
        {
            tutorialPanel.SetActive(false);
            OnClose?.Invoke();
            OnClose = null;
        }
    }
}