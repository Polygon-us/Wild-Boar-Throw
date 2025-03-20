using Gameplay.Controllers;
using Unity.Cinemachine;
using UnityEngine;

namespace Gameplay.ThrowStates
{
    public class AngleState : StateBase
    {
        private const string AngleMessage = "Apuntando";

        [SerializeField] private AngleController angleController;
        [SerializeField] private CountdownController countdownController;
        [SerializeField] private CamerasController camerasController;
        [SerializeField] private ThrowManager throwManager;
        [SerializeField] private CinemachineCamera followCamera;
        [SerializeField] private AnimationCurve cameraBounceCurve;

        private LTDescr pingPongTween;
        private float angle;

        private bool _clicked;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            _clicked = false;
            
            camerasController.FollowCamera();
            
            countdownController.Open(angleController.AngleSelectionTime, AngleMessage);
            countdownController.StartCountDown();
            
            pingPongTween = LeanTween.value(angleController.MinAngle, angleController.MaxAngle,
                    angleController.AnglePingPongTime)
                .setOnUpdate(t =>
                {
                    angle = t;
                    angleController.AngleSlider.value = angle;
                })
                .setLoopPingPong()
                .setOnComplete(Blunder);
        }

        public override void OnExitState()
        {
            countdownController.Close();
            LeanTween.cancel(pingPongTween.uniqueId);
        }

        public override void OnClick()
        {
            if (_clicked)
                return;
             
            _clicked = true;
            
            throwManager.Angle = angle;

            LeanTween.cancel(pingPongTween.uniqueId);

            LeanTween.value(0, 1, 1).setOnUpdate((t) =>
            {
                float bounceValue = cameraBounceCurve.Evaluate(t);
                followCamera.Lens.FieldOfView = bounceValue;
            }).setOnComplete(() => throwManager.NextState());
        }

        public override void OnReset()
        {
            angleController.Reset();

            angle = 0;

            if (pingPongTween != null)
                LeanTween.cancel(pingPongTween.uniqueId);
        }

        private void Blunder()
        {
            angle = 0;

            throwManager.Force /= 2;

            angleController.Blunder();

            OnClick();
        }
    }
}