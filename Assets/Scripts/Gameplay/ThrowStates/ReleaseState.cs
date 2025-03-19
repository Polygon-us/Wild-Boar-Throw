using ForceVisualizerAnimation;
using UnityEngine;
using UI.Gameplay;

namespace Gameplay.ThrowStates
{
    public class ReleaseState : StateBase
    {
        [SerializeField] private ThrowManager throwManager;
        [SerializeField] private DistanceFollow distanceFollow;
        [SerializeField] private BoarThrower boarThrower;

        [SerializeField] private ForceVisualizerController forceVisualizerController;
        [SerializeField] private Boar boar;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            boar.OnCollision += CallOnCollision;

            throwManager.Release();

            distanceFollow.EnableDistanceText(true);

            boarThrower.ThrowBoar(throwManager.Force, throwManager.Angle);

            forceVisualizerController.MovePlayableDirector(0);
            forceVisualizerController.PlayThrowAnimation();
        }

        public override void OnExitState()
        {
            boar.OnCollision -= CallOnCollision;
        }

        public override void OnReset()
        {
            distanceFollow.Reset();
        }

        private void CallOnCollision()
        {
            LeanTween.delayedCall(2, StateMachine.NextState);
        }
    }
}