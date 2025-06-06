using GoogleMobileAds.Api;
using UnityEngine;
using Ads;

namespace Gameplay.ThrowStates
{
    public class AdState : StateBase
    {
        [SerializeField] private float adProbability = 0.3f;
        
        private InterstitialAd interstitialAd;
        
        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            if (Random.Range(0f, 1f) > adProbability)
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
                // TODO: Mute game?
            };
            // Raised when the ad closed full-screen content.
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                // TODO: Resume game
                
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