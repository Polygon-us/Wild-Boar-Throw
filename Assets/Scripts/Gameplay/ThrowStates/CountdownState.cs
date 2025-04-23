using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.ThrowStates
{
    public class CountdownState : StateBase
    {
        [SerializeField] private CountdownController countdownController;
        [SerializeField] private AngleController angleController;
        [SerializeField] private CamerasController camerasController;
        
        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);
            
            camerasController.FollowCamera();
            
            angleController.Show();
            countdownController.Open();
            countdownController.StartCountDown(StateMachine.NextState);
        }

        public override void OnExitState()
        {
            countdownController.Close();
        }
        
        public override void OnReset()
        {
            countdownController.Reset();
        }
    }
}