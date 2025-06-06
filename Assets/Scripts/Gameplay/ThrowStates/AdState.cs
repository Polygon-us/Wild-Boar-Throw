using GoogleMobileAds.Api;
using UnityEngine;
using Ads;

namespace Gameplay.ThrowStates
{
    public class AdState : StateBase
    {
        [SerializeField] private float adProbability = 0.3f;
        
        private InterstitialAd interstitialAd;
        
        private bool isInitialized = false;
        
        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            if (!isInitialized)
            {
                isInitialized = true;
                stateMachine.NextState();
                return;
            }
            
            if (Random.value > adProbability)
            {
                stateMachine.NextState();
                return;
            }
            
            AdsManager.CleanUpAdd(ref interstitialAd);
            
            var adRequest = new AdRequest();

            InterstitialAd.Load(AdIds.InterstitialID, adRequest, OnAdLoaded);
        }

        private void OnAdLoaded(InterstitialAd ad, LoadAdError error)
        {
            if (error != null || ad == null)
            {
                Debug.Log("Interstitial ad not loaded");
                StateMachine.NextState();
                return;
            }

            interstitialAd = ad;
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
                
                StateMachine.NextState();
            };
            
            // Raised when the ad failed to open full-screen content.
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);
                
                StateMachine.NextState();
            };
        }
        
        public override void OnExitState()
        {
            base.OnExitState();
        }
    }
}