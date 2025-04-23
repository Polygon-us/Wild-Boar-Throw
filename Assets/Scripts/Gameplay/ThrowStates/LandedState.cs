using Gameplay.Controllers;
using Unity.Cinemachine;
using UnityEngine;

namespace Gameplay.ThrowStates
{
    public class LandedState : StateBase
    {
        [SerializeField] private CamerasController cameraController;
        [SerializeField] private Transform hideOnLanding;
        [SerializeField] private CinemachineTargetGroup targetGroup;
        [SerializeField] private float landingCamTransitionDuration = 2f;
        [SerializeField] private Vector3 hideOnLandingPosition = new Vector3(-35f, 0.2f, 70f);
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

                    /*if (value <= 0.5)
                    {
                        hideOnLanding.localPosition = new Vector3(-35f, 0.2f, 70f);
                        hideOnLanding.eulerAngles = new Vector3(0, 0, 20);
                    }*/
                })
                .setOnComplete(StateMachine.NextState)
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

        public override void OnReset()
        {
            targetGroup.Targets[^1].Weight = 1;
            targetGroup.Targets[0].Weight = 0;
            
            LeanTween.cancel(_tween);
        }
    }
}