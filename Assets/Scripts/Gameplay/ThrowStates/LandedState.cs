using Gameplay.Controllers;
using Unity.Cinemachine;
using PlayServices;
using UnityEngine;

namespace Gameplay.ThrowStates
{
    public class LandedState : StateBase
    {
        [SerializeField] private CamerasController cameraController;
        [SerializeField] private Transform hideOnLanding;
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
                .setOnComplete(OnLandedComplete)
                .uniqueId;

            if(BThrower.BoarDistance< 15f)
            {
                AudioManager.Instance.PlayUI("BadShot");
            }
            else
            {
                AudioManager.Instance.PlayUI("NiceShot");

            }
        }

        private void OnLandedComplete()
        {
            Leaderboard.PostLeaderboard(BThrower.BoarDistance);
            
            StateMachine.NextState();
        }

        public override void OnReset()
        {
            targetGroup.Targets[^1].Weight = 1;
            targetGroup.Targets[0].Weight = 0;
            
            LeanTween.cancel(_tween);
        }
    }
}