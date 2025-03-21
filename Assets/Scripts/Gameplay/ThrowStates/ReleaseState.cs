using ForceVisualizerAnimation;
using UnityEngine;
using UI.Gameplay;

namespace Gameplay.ThrowStates
{
    public class ReleaseState : StateBase
    {
        [SerializeField] private ThrowManager throwManager;
        [SerializeField] private DistanceFollowView distanceFollowView;
        [SerializeField] private BoarThrower boarThrower;
        [SerializeField] private ForceVisualizerController forceVisualizerController;

        private int _delayCallTween;

        public override void OnEnterState(StateMachine stateMachine)
        {
            base.OnEnterState(stateMachine);

            boarThrower.OnCollision += CallOnCollision;

            throwManager.Release();

            distanceFollowView.EnableDistanceText(true);

            boarThrower.ThrowBoar(throwManager.Force, throwManager.Angle);

            forceVisualizerController.MovePlayableDirector(0);
            forceVisualizerController.PlayThrowAnimation();
        }

        public override void OnExitState()
        {
            boarThrower.OnCollision -= CallOnCollision;
        }

        public override void OnReset()
        {
            distanceFollowView.Reset();

            LeanTween.cancel(_delayCallTween);
        }

        private void CallOnCollision()
        {
            _delayCallTween = LeanTween.delayedCall(2, StateMachine.NextState).uniqueId;
        }
    }
}