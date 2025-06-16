using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayServices;
using UnityEngine;

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

        private static void ShowLeaderboard()
        {
            Leaderboard.ShowLeaderboard();
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}