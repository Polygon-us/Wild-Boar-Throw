using Unity.Cinemachine;
using UnityEngine;

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

        LeanTween.value(gameObject, 1f, 0f, landingCamTransitionDuration)
            .setEase(LeanTweenType.easeInOutCubic).setDelay(1.5f)
            .setOnUpdate((value) =>
            {
                targetGroup.Targets[^1].Weight = value + 1;
                targetGroup.Targets[0].Weight = 1 - value;

                if (value <= 0.5)
                {
                    hideOnLanding.localPosition = new Vector3(-35f, 0.2f, 70f);
                    hideOnLanding.eulerAngles = new Vector3(0, 0, 20);
                }
            });
    }

    public override void OnExitState()
    {
        base.OnExitState();
        
        targetGroup.Targets[^1].Weight = 1;
        targetGroup.Targets[0].Weight = 0;

        hideOnLanding.localPosition = new Vector3(-21.5f, 0f, 70.5f);
        hideOnLanding.eulerAngles = new Vector3(0, 0, 0);
    }
}