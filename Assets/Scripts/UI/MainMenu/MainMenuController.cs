using UnityEngine.SceneManagement;
using PlayServices;
using UnityEngine;
using General;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MainMenuPanel mainMenuPanel;
        [SerializeField] private SettingsPanel settingsPanel;

        private void OnEnable()
        {
            mainMenuPanel.OnStartGame += StartGame;
            mainMenuPanel.OnLeaderboard += ShowLeaderboard;
            mainMenuPanel.OnSettings += ShowSettingsPanel;
            mainMenuPanel.OnExitGame += ExitGame;

            settingsPanel.OnBack += ShowMainMenuPanel;
        }

        private void OnDisable()
        {
            mainMenuPanel.OnStartGame -= StartGame;
            mainMenuPanel.OnLeaderboard -= ShowLeaderboard;
            mainMenuPanel.OnSettings -= ShowSettingsPanel;
            mainMenuPanel.OnExitGame -= ExitGame;

            settingsPanel.OnBack -= ShowMainMenuPanel;
        }

        private void Start()
        {
            ShowMainMenuPanel();
            
            settingsPanel.SetCurrentVolume();
            
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

        private void ShowMainMenuPanel()
        {
            mainMenuPanel.gameObject.SetActive(true);
            settingsPanel.gameObject.SetActive(false);
        }

        private void ShowSettingsPanel()
        {
            settingsPanel.gameObject.SetActive(true);
            mainMenuPanel.gameObject.SetActive(false);
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