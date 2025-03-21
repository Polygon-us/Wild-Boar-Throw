using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.ThrowStates
{
    public class TutorialState : StateBase
    {
        [SerializeField, TextArea] private string tutorialMsg;
        [Space]
        [SerializeField] private TutorialController tutorialController;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            tutorialController.Open(tutorialMsg, StateMachine.NextState);
        }
    }
}