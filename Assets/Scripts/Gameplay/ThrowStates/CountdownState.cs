using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.ThrowStates
{
    public class CountdownState : StateBase
    {
        [SerializeField] private CountdownController countdownController;
        
        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);
            
            countdownController.Open(string.Empty);
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