using UnityEngine.SceneManagement;
using UnityEngine;
using UI.PopUp;
using General;

namespace Gameplay.Controllers
{
    public class PauseController : MonoBehaviour
    {
        private bool isPaused;

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
                "Do you want to return to main menu?",
                "Exit",
                "Continue",
                onYesAction: GoToMainMenu,
                onNoAction: Resume
            );
        }

        private void GoToMainMenu()
        {
            Resume();
            SceneManager.LoadScene(1);
        }
    }
}