using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class AdsManager : MonoBehaviour
    {
        private const string BannerID = "ca-app-pub-8907326292508524/8644264397";
        private const string LevelCompletionID = "ca-app-pub-8907326292508524/1006658971";
        
        private void Start()
        {
            MobileAds.Initialize(OnInitializationComplete);
        }

        private void OnInitializationComplete(InitializationStatus status)
        {
        }

        private void ShowBanner()
        {
            BannerView bannerView = new BannerView(BannerID, AdSize.Banner, AdPosition.Top);
            
        }
    }
}