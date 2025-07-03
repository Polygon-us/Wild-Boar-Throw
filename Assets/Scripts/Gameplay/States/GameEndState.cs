using Gameplay.Controllers;
using PlayServices;
using UnityEngine;

namespace Gameplay.States
{
    public class GameEndState : StateBase
    {
        [SerializeField] private GameEndController gameEndController;
        [SerializeField] private PauseController pauseController;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            gameEndController.Open();
            gameEndController.OnRestart += OnRestart;
            gameEndController.OnLeaderboard += OnLeaderboard;
            gameEndController.OnMainMenu += OnMainMenu;
        }

        public override void OnExitState()
        {
            base.OnExitState();

            gameEndController.Close();
            gameEndController.OnRestart -= OnRestart;
            gameEndController.OnLeaderboard -= OnLeaderboard;
            gameEndController.OnMainMenu -= OnMainMenu;
        }

        private void OnRestart()
        {
            StateMachine.OnReset();
        }

        private void OnLeaderboard()
        {
            Leaderboard.ShowLeaderboard();
        }

        private void OnMainMenu()
        {
            pauseController.GoToMainMenu();
        }
    }
}