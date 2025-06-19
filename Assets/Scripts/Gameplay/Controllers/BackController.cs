using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

namespace Gameplay.Controllers
{
    public class BackController : MonoBehaviour
    {
        [SerializeField] private RectTransform exitPanel;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button continueButton;
        
        private InputAction exitAction;

        private bool isPaused;

        private void Awake()
        {
            isPaused = false;
            
            exitButton.onClick.AddListener(OnExit);
            continueButton.onClick.AddListener(OnContinue);
        }

        private void Start()
        {
            Close();

            exitAction = InputSystem.actions.FindAction("Exit");
            exitAction.Enable();
            exitAction.started += OnBackClicked;
        }

        private void OnBackClicked(InputAction.CallbackContext context)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        
        private void OnExit()
        {
            Resume();
            GoToMainMenu();
        }
        
        private void OnContinue()
        {
            Resume();
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
            Close();
        }

        private void Open()
        {
            exitPanel.gameObject.SetActive(true);
        }

        private void Close()
        {
            exitPanel.gameObject.SetActive(false);
        }

        private static void GoToMainMenu()
        {
            SceneManager.LoadScene(1);
        }
        
        private void OnDisable()
        {
            exitAction.performed -= OnBackClicked;
            exitAction.Disable();
        }
    }
}