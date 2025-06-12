using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UI.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            startButton.onClick.AddListener(StartGame);
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