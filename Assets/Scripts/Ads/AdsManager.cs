#if ENABLE_ADS
using Cysharp.Threading.Tasks;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Ads
{
    public class AdsManager : MonoBehaviour
    {
        public async UniTask LoadAds()
        {
#if ENABLE_ADS
            var tcs = new UniTaskCompletionSource();
            MobileAds.Initialize(_ => tcs.TrySetResult());
            await tcs.Task;
#endif
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