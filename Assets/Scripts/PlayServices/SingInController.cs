using GooglePlayGames.BasicApi;
using Cysharp.Threading.Tasks;
using GooglePlayGames;
using UnityEngine;
using UI.Login;

namespace PlayServices
{
    public class SingInController : MonoBehaviour
    {
        [SerializeField] private SingInView view;

        private UniTaskCompletionSource completionSource;
        
        public async UniTask SignIn()
        {
            PlayGamesPlatform.Activate();
   
            view.ShowLoading();

#if UNITY_EDITOR
            return;
#else 
            completionSource = new UniTaskCompletionSource();
            
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);

            await completionSource.Task;
#endif
        }

        private void ProcessAuthentication(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                completionSource.TrySetResult();
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