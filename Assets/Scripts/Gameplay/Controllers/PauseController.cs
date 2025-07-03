using UnityEngine.SceneManagement;
using UnityEngine;
using UI.PopUp;
using General;
using Utils;

namespace Gameplay.Controllers
{
    public class PauseController : MonoBehaviour
    {
        [Header("Popup")] [SerializeField] private PopupLocalizedText popupTexts;

        private static bool isPaused;
        
        public static bool IsPaused => isPaused;

        private void OnEnable()
        {
            ExitController.AddAction(TogglePause);
        }

        private void OnDisable()
        {
            ExitController.AddAction(TogglePause);
        }

        private void Awake()
        {
            isPaused = false;
        }

        private void TogglePause()
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }

        private void Pause()
        {
            Time.timeScale = 0;
            isPaused = true;
            Open();
        }

        private void Resume()
        {
            Time.timeScale = 1;
            isPaused = false;
        }

        private void Open()
        {
            YesNoPopUp.Instance.Open
            (
                popupTexts.popupMessage.GetLocalizedString(),
                popupTexts.popupYesTxt.GetLocalizedString(),
                popupTexts.popupNoTxt.GetLocalizedString(),
                onYesAction: GoToMainMenu,
                onNoAction: Resume
            );
        }

        public void GoToMainMenu()
        {
            Resume();
            SceneManager.LoadScene(1);
        }
    }
}