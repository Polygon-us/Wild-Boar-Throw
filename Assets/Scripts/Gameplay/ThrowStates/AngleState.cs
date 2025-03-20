using Gameplay.Controllers;
using Unity.Cinemachine;
using UnityEngine;

namespace Gameplay.ThrowStates
{
    public class AngleState : StateBase
    {
        [SerializeField] private AngleController angleController;
        [SerializeField] private CountdownController countdownController;
        [SerializeField] private CamerasController camerasController;
        [SerializeField] private ThrowManager throwManager;
        [SerializeField] private CinemachineCamera followCamera;
        [SerializeField] private AnimationCurve cameraBounceCurve;
        
        private bool _clicked;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            _clicked = false;
            
            camerasController.FollowCamera();
            
            countdownController.StartCountDown(Blunder);

            angleController.StartTween();
        }

        public override void OnExitState()
        {
            countdownController.Close();
            angleController.StopTween();
        }

        public override void OnClick()
        {
            if (_clicked)
                return;
             
            _clicked = true;
            
            countdownController.StopCountDown();
            angleController.StopTween();
            
            throwManager.Angle = angleController.Angle; 
            
            LeanTween.value(0, 1, 1).setOnUpdate((t) =>
            {
                float bounceValue = cameraBounceCurve.Evaluate(t);
                followCamera.Lens.FieldOfView = bounceValue;
            }).setOnComplete(() => throwManager.NextState());
        }

        public override void OnReset()
        {
            angleController.Reset();
        }

        private void Blunder()
        {
            throwManager.Force /= 2;

            angleController.Blunder();

            OnClick();
        }
    }
}