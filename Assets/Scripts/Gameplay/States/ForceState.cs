using ForceVisualizerAnimation;
using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.States
{
    public class ForceState : StateBase
    {
        [SerializeField] private ForceController forceController;
        [SerializeField] private CountdownController countdownController;
        [SerializeField] private ThrowManager throwManager;
        [SerializeField] private ForceVisualizerController forceVisualizerController;
        [SerializeField] private BoarThrower boarThrower;
        [SerializeField] private CamerasController camerasController;
        [SerializeField] private CrowdController crowdController;

        private float _force;
        private int _numClicks;
        
        private bool _firstClick = false;


        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);
            
            forceController.ClickBtn.onClick.AddListener(FirstClick);
            forceController.ClickBtn.gameObject.SetActive(true);

            forceController.StateText.StringReference.Arguments = new object[] { _numClicks };
            forceController.StateText.StringReference.RefreshString();

            forceController.Show();
            
            forceVisualizerController.MovePlayableDirector(0);
        }

        public override void OnExitState()
        {
            forceController.ClickBtn.onClick.RemoveListener(FirstClick);
        }

        public override void OnUpdate()
        {
            if (!_firstClick)
                return;
            
            UpdateForce(-forceController.MaxForce * forceController.DecrementPercentage * Time.deltaTime);

            boarThrower.MoveBoarWithStartingPosition();
        }

        private void FirstClick()
        {
            _firstClick = true;
            forceController.ClickBtn.onClick.RemoveListener(FirstClick);
            forceController.ClickBtn.gameObject.SetActive(false);
            
            countdownController.StartCountDown(Release);
        }

        public override void OnClick()
        {
            if (!_firstClick)
                return;

            _numClicks++;

            float forceResistance = forceController.ChargeCurve.Evaluate(_force / forceController.MaxForce);

            UpdateForce(forceResistance * forceController.MaxForce * forceController.IncrementPercentage);

            forceController.StateText.StringReference.Arguments[0] = _numClicks;;
            forceController.StateText.StringReference.RefreshString();

            float pitch = 1f + (_numClicks * 0.2f);
            pitch = Mathf.Clamp(pitch, 1f, 5f);
            AudioManager.Instance.PlaySFX("Charge", pitch);
        }

        private void UpdateForce(float delta)
        {
            _force = Mathf.Clamp(_force + delta, 0f, forceController.MaxForce);

            forceVisualizerController.MovePlayableDirector(_force / forceController.MaxForce);
        }

        private void Release()
        {
            throwManager.Force = _force;

            crowdController.MakeImpression(_force / forceController.MaxForce);

            StateMachine.NextState();
        }

        public override void OnReset()
        {
            _force = 0;
            _numClicks = 0;
            _firstClick = false;
            forceVisualizerController.Restart();
            countdownController.Reset();
            crowdController.MakeImpression(0);
            camerasController.Reset();
            boarThrower.Reset();
        }
    }
}