using UnityEngine;

namespace Gameplay.States
{
    public class FinishTutorialState : StateBase
    {
        private const string TutorialKey = "Tutorial";
        
        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);
            
            PlayerPrefs.SetInt(TutorialKey, 1);
            StateMachine.NextState();
        }
    }
}