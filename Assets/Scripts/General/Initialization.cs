using UnityEngine.Localization.Settings;
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
        [SerializeField] private SoundController soundController;
        
        private async UniTaskVoid Start()
        {
            Application.targetFrameRate = 60;
            
            soundController.SetCurrentVolume();
            
            await LocalizationSettings.InitializationOperation;
            
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