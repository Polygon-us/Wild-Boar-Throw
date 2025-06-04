using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine;

namespace PlayServices
{
    public class SingInController : MonoBehaviour
    {
        private void Start()
        {
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

        private static void ProcessAuthentication(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                // TODO: retry
                // PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication)
            }
        }
    }
}