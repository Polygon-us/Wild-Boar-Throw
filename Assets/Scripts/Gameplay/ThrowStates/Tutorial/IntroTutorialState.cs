using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.ThrowStates.Tutorial
{
    public class IntroTutorialState : StateBase
    {
        [SerializeField] private TutorialController tutorialController;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            tutorialController.Open(StateMachine.NextState);
        }
    }
}