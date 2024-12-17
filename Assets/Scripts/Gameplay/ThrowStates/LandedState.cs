using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class LandedState : StateBase
{
    [SerializeField] private CamerasController cameraController;
    [SerializeField] private Transform hideOnLanding;
    [SerializeField] private CinemachineTargetGroup targetGroup;
    [SerializeField] private float landingCamTransitionDuration = 2f;

    public override void OnEnterState(StateMachine stateMachine)
    {
        base.OnEnterState(stateMachine);
        
        cameraController.ShowLanding();

        LeanTween.value(this.gameObject, 1f, 0f, landingCamTransitionDuration)
            .setEase(LeanTweenType.easeInOutCubic)
            .setOnUpdate((value) =>
            {
                targetGroup.Targets[^1].Weight = value + 1;
                targetGroup.Targets[0].Weight = 1 - value;

                if (value <= 0.5)
                {
                    hideOnLanding.localPosition = new Vector3(-13, 11, 0);
                    hideOnLanding.eulerAngles = new Vector3(0, 0, 36);
                }
            });
    }

    public override void OnExitState()
    {
        base.OnExitState();

        hideOnLanding.localPosition = new Vector3(14.7f, 0, 0);
        hideOnLanding.eulerAngles = new Vector3(0, 0, 0);
    }
}