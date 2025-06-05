using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;
using UI.Login;

namespace PlayServices
{
    public class SingInController : MonoBehaviour
    {
        [SerializeField] private SingInView view;

        private void Awake()
        {
            //Initialize PlayGamesPlatform
            PlayGamesPlatform.Activate();
        }
        
        private void Start()
        {
            view.ShowLoading();

            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

        private void ProcessAuthentication(SignInStatus status)
        {
            print($"Login status: {status}");
            
            if (status == SignInStatus.Success)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                view.ShowLogin(() =>
                {
                    view.ShowLoading();
                    PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
                });
            }
        }
    }
}