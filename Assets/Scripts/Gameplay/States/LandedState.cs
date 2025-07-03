using Gameplay.Controllers;
using PlayServices;
using Unity.Cinemachine;
using UnityEngine;

namespace Gameplay.States
{
    public class LandedState : StateBase
    {
        private const string BadShotTxt = "BadShot";
        private const string NiceShotTxt = "NiceShot";
        
        [SerializeField] private CamerasController cameraController;
        [SerializeField] private CinemachineTargetGroup targetGroup;
        [SerializeField] private float landingCamTransitionDuration = 2f;
        [SerializeField] private BoarThrower BThrower;

        private int _tween;
        
        public override void OnEnterState(StateMachine stateMachine)
        {  
            base.OnEnterState(stateMachine);

            targetGroup.Targets[^1].Weight = 1;
            targetGroup.Targets[0].Weight = 0;
            
            cameraController.ShowLanding();

            _tween = LeanTween.value(gameObject, 1f, 0f, landingCamTransitionDuration)
                .setEase(LeanTweenType.easeInOutCubic)
                .setDelay(1f) // Delay needs to be the same as in the cinemachine custom blends
                .setOnUpdate((value) =>
                {
                    targetGroup.Targets[^1].Weight = value + 1;
                    targetGroup.Targets[0].Weight = 1 - value;
                })
                .setOnComplete(StateMachine.NextState)
                .uniqueId;

            AudioManager.Instance.PlayUI(BThrower.BoarDistance < 15f ? BadShotTxt : NiceShotTxt);
            
            Leaderboard.PostLeaderboard(BThrower.BoarDistance);
        }

        public override void OnReset()
        {
            targetGroup.Targets[^1].Weight = 1;
            targetGroup.Targets[0].Weight = 0;
            
            LeanTween.cancel(_tween);
        }
    }
}