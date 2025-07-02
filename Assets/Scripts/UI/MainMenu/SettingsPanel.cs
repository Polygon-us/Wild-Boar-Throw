using UnityEngine.UI;
using UnityEngine;
using General;
using System;

namespace UI.MainMenu
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private Button backBtn;

        public Action OnBack;
        
        private void OnEnable()
        {
            ExitController.AddAction(OnBackClicked);
        }

        private void OnDisable()
        {
            ExitController.RemoveAction(OnBackClicked);
        }

        private void Awake()
        {
            backBtn.onClick.AddListener(OnBackClicked);
        }

        private void OnBackClicked()
        {
            OnBack?.Invoke();
        }
    }
}