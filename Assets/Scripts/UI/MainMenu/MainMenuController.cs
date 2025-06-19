using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayServices;
using UnityEngine;
using UI.PopUp;
using General;
using Utils;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button leaderboardButton;
        [SerializeField] private Button exitButton;

        [Header("Popup")] 
        [SerializeField] private PopupLocalizedText popupTexts;
        
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
            startButton.onClick.AddListener(StartGame);
            leaderboardButton.onClick.AddListener(ShowLeaderboard);
            exitButton.onClick.AddListener(ExitGame);

            EnableMobileKeyboard(false);
        }

        public void EnableMobileKeyboard(bool on)
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            WebGLInput.mobileKeyboardSupport = on;
#endif
        }

        private void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void ShowCloseGameAlert()
        {
            YesNoPopUp.Instance.Open
            (
                popupTexts.popupMessage.GetLocalizedString(),
                popupTexts.popupYesTxt.GetLocalizedString(),
                popupTexts.popupNoTxt.GetLocalizedString(),
                ExitGame
            );
        }
        
        private static void ShowLeaderboard()
        {
            Leaderboard.ShowLeaderboard();
        }

        private static void ExitGame()
        {
            ExitController.ExitGame();
        }
    }
}