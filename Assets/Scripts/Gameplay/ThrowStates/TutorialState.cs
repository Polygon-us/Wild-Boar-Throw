using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.ThrowStates.Tutorial
{
    public class TutorialState : StateBase
    {
        [SerializeField] private string tutorialMsg;
        [SerializeField] private TutorialController tutorialController;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            tutorialController.Open(tutorialMsg, StateMachine.NextState);
        }
    }
}