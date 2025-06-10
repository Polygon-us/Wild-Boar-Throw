#if ENABLE_ADS
using GoogleMobileAds.Api;
#endif

using UnityEngine;
using Ads;

namespace Gameplay.ThrowStates
{
    public class AdState : StateBase
    {
        [SerializeField] private float adProbability = 0.3f;
        
#if ENABLE_ADS
        private InterstitialAd interstitialAd;

        private bool isInitialized = false;
#endif
        
        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);
        
#if ENABLE_ADS  
            if (!isInitialized)
            {
                isInitialized = true;
                
                LoadInterstitialAd();
                
                stateMachine.NextState();
                return;
            }
          
            if (Random.value > adProbability)
            {
                stateMachine.NextState();
                return;
            }

            if (interstitialAd != null && interstitialAd.CanShowAd())
                ShowInterstitialAd();
#else
            stateMachine.NextState();
#endif    
        }  

#if ENABLE_ADS
        private void LoadInterstitialAd()
        {
            AdsManager.CleanUpAdd(ref interstitialAd);
            
            var adRequest = new AdRequest();

            InterstitialAd.Load(AdIds.InterstitialID, adRequest, OnAdLoaded);
        }

        private void OnAdLoaded(InterstitialAd ad, LoadAdError error)
        {
            if (error != null || ad == null)
            {
                Debug.Log("Interstitial ad not loaded");
                return;
            }

            interstitialAd = ad;
        }

        private void ShowInterstitialAd()
        {
            interstitialAd.Show();
            
            AddListeners();
        }
        
        private void AddListeners()
        {
            interstitialAd.OnAdFullScreenContentOpened += () =>
            {
                AudioManager.Instance.ToggleMute();
            };
            
            // Raised when the ad closed full-screen content.
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                AudioManager.Instance.ToggleMute();
                
                LoadInterstitialAd();
                
                StateMachine.NextState();
            };
            
            // Raised when the ad failed to open full-screen content.
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);
                
                LoadInterstitialAd();
                
                StateMachine.NextState();
            };
        }
#endif    
        
        public override void OnExitState()
        {
            base.OnExitState();
        }
    }
}