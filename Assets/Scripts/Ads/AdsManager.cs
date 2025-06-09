#if ENABLE_ADS
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class AdsManager : MonoBehaviour
    {
        private void Start()
        {
            #if Enable_Ads
            MobileAds.Initialize(OnInitializationComplete);
            #endif
        }

        private void OnInitializationComplete(InitializationStatus status)
        {
        }

        public static void CleanUpAdd(ref InterstitialAd interstitialAd)
        {
            if (interstitialAd == null)
                return;
            
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        public static void CleanUpAdd(ref BannerView bannerView)
        {
            if (bannerView == null)
                return;
            
            bannerView.Destroy();
            bannerView = null;
        }
    }
}
#endif