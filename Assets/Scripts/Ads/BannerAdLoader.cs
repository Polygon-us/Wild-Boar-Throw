using GoogleMobileAds.Api;
using UnityEngine;
using UI.Generic;

namespace Ads
{
    public class BannerAdLoader : MonoBehaviour
    {
        private BannerView bannerView;

        private void Start()
        {
            AdsManager.CleanUpAdd(ref bannerView);   
            
            bannerView = new BannerView(AdIds.BannerID, AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth), AdPosition.Top);

            AddListeners();

            bannerView.LoadAd(new AdRequest());
        }

        private void AddListeners()
        {
            bannerView.OnBannerAdLoaded += () => SafeArea.SetAdSafeArea(bannerView.GetHeightInPixels());
        }
        
        private void OnDestroy()
        {
            AdsManager.CleanUpAdd(ref bannerView);
        }
    }
}