using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.States
{
    public class AngleState : StateBase
    {
        [SerializeField] private AngleController angleController;
        [SerializeField] private CountdownController countdownController;
        [SerializeField] private CamerasController camerasController;
        [SerializeField] private ForceController forceController;
        [SerializeField] private ThrowManager throwManager;
        [SerializeField] private AnimationCurve cameraBounceCurve;
        
        private bool _clicked;
        private int _tween;
        
        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            _clicked = false;
            
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
            
            forceController.Hide();
            angleController.Hide();
            
            throwManager.Angle = angleController.Angle;

            AudioManager.Instance.PlayUI("SetAngle");
            
            _tween = LeanTween.value(0, 1, 1)
                .setOnUpdate(t =>
                {
                    float bounceValue = cameraBounceCurve.Evaluate(t);
                    camerasController.CurrentCamera.Lens.FieldOfView = bounceValue;
                })
                .setOnComplete(() => StateMachine.NextState())
                .uniqueId;
        }

        public override void OnReset()
        {
            LeanTween.cancel(_tween);
            
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