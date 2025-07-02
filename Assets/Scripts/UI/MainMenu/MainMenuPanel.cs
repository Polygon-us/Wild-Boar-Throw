using UnityEngine.UI;
using UnityEngine;
using UI.PopUp;
using General;
using System;
using Utils;

namespace UI.MainMenu
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button leaderboardButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;

        [Header("Popup")] [SerializeField] private PopupLocalizedText popupTexts;

        public Action OnStartGame;
        public Action OnLeaderboard;
        public Action OnSettings;
        public Action OnExitGame;

        private void OnEnable()
        {
            ExitController.AddAction(ShowCloseGameAlert);
        }

        private void OnDisable()
        {
            ExitController.RemoveAction(ShowCloseGameAlert);
        }

        private void Awake()
        {
            startButton.onClick.AddListener(() => OnStartGame?.Invoke());
            leaderboardButton.onClick.AddListener(() => OnLeaderboard?.Invoke());
            settingsButton.onClick.AddListener(() => OnSettings?.Invoke());
            exitButton.onClick.AddListener(() => OnExitGame?.Invoke());
        }
        
        private void ShowCloseGameAlert()
        {
            YesNoPopUp.Instance.Open
            (
                popupTexts.popupMessage.GetLocalizedString(),
                popupTexts.popupYesTxt.GetLocalizedString(),
                popupTexts.popupNoTxt.GetLocalizedString(),
                OnExitGame
            );
        }
    }
}