using UnityEngine.Localization;
using Gameplay.Controllers;
using UnityEngine;

namespace Gameplay.ThrowStates
{
    public class TutorialState : StateBase
    {
        private const string TutorialKey = "Tutorial";
        
        [SerializeField] private LocalizedString tutorialMsg;
        [Space]
        [SerializeField] private TutorialController tutorialController;
        
        private static bool IsTutorialDone => PlayerPrefs.GetInt(TutorialKey, 0) == 1;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);
            
            if (IsTutorialDone)
                StateMachine.NextState();
            else
                tutorialController.Open(tutorialMsg.GetLocalizedString(), StateMachine.NextState);
        }
        
    }
}