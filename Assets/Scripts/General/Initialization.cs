using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using PlayServices;
using UnityEngine;
using Ads;

namespace General
{
    public class Initialization : MonoBehaviour
    {
        [SerializeField] private SingInController singInController;
        [SerializeField] private AdsManager adsManager;
        
        private async UniTaskVoid Start()
        {
            await singInController.SignIn();
            
#if ENABLE_ADS
            await adsManager.LoadAds();
#endif
            
            LoadFirstLevel();
        }

        private static void LoadFirstLevel()
        {
            SceneManager.LoadScene(1);
        }
    }
}