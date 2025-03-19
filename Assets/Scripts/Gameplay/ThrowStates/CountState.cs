using UnityEngine;
using UI.Gameplay;

namespace Gameplay.ThrowStates
{
    public class CountState : StateBase
    {
        [SerializeField] private CountController countController;
        [SerializeField] private CamerasController camerasController;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            countController.Open();

            camerasController.FollowCamera();

            countController.StartCountDown(OnCountDownFinished);
        }

        private void OnCountDownFinished()
        {
            countController.Close();

            StateMachine.NextState();
        }
    }
}