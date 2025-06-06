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

#if UNITY_EDITOR
            SceneManager.LoadScene(1);
#else
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
#endif
        }

        private void ProcessAuthentication(SignInStatus status)
        {
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