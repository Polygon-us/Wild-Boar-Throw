using ForceVisualizerAnimation;
using UnityEngine;
using UI.Gameplay;
using UnityEngine.Serialization;

namespace Gameplay.ThrowStates
{
    public class ReleaseState : StateBase
    {
        [SerializeField] private ThrowManager throwManager;
        [FormerlySerializedAs("distanceFollow")] [SerializeField] private DistanceFollowView distanceFollowView;
        [SerializeField] private BoarThrower boarThrower;

        [SerializeField] private ForceVisualizerController forceVisualizerController;
        [SerializeField] private Boar boar;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            boar.OnCollision += CallOnCollision;

            throwManager.Release();

            distanceFollowView.EnableDistanceText(true);

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
            distanceFollowView.Reset();
        }

        private void CallOnCollision()
        {
            LeanTween.delayedCall(2, StateMachine.NextState);
        }
    }
}