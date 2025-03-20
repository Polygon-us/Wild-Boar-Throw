using ForceVisualizerAnimation;
using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.ThrowStates
{
    public class ForceState : StateBase
    {
        [SerializeField] private ForceController forceController;
        [SerializeField] private ThrowManager throwManager;
        [SerializeField] private ForceVisualizerController forceVisualizerController;
        [SerializeField] private BoarThrower boarThrower;
        [SerializeField] private CamerasController camerasController;
        [SerializeField] private CrowdController crowdController;

        private float force;
        private float chargeTimer;
        private int numClicks;
        
        private bool firstClick = false;


        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            forceController.ClickBtn.onClick.AddListener(FirstClick);
            forceController.ClickBtn.gameObject.SetActive(true);
            
            forceController.StateText.text = $"Clicks {numClicks}";

            forceVisualizerController.MovePlayableDirector(0);
        }

        public override void OnExitState()
        {
        }

        public override void OnUpdate()
        {
            if (!firstClick)
                return;

            chargeTimer += Time.deltaTime;

            UpdateForce(-forceController.MaxForce * forceController.DecrementPercentage * Time.deltaTime);

            boarThrower.MoveBoarWithStartingPosition();

            if (chargeTimer >= forceController.ForceChargeTime)
            {
                Release();
                chargeTimer = 0f;
            }
        }

        private void FirstClick()
        {
            firstClick = true;
            forceController.ClickBtn.onClick.RemoveListener(FirstClick);
            forceController.ClickBtn.gameObject.SetActive(false);
        }

        public override void OnClick()
        {
            if (!firstClick)
                return;

            numClicks++;

            float forceResistance = forceController.ChargeCurve.Evaluate(force / forceController.MaxForce);

            UpdateForce(forceResistance * forceController.MaxForce * forceController.IncrementPercentage);

            forceController.StateText.text = $"Clicks {numClicks}";
        }

        private void UpdateForce(float delta)
        {
            force = Mathf.Clamp(force + delta, 0f, forceController.MaxForce);

            forceVisualizerController.MovePlayableDirector(force / forceController.MaxForce);
        }

        private void Release()
        {
            throwManager.Force = force;

            crowdController.MakeImpression(force / forceController.MaxForce);

            StateMachine.NextState();
        }

        public override void OnReset()
        {
            force = 0;
            chargeTimer = 0;
            numClicks = 0;
            firstClick = false;
            forceVisualizerController.MovePlayableDirector(0);
            crowdController.MakeImpression(0);
            camerasController.Reset();
            boarThrower.Reset();
        }
    }
}