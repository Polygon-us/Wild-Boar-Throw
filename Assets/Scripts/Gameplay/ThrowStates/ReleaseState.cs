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
        [SerializeField] private Boar boar;

        private int _delayCalleTween;

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

            LeanTween.cancel(_delayCalleTween);
        }

        private void CallOnCollision()
        {
            _delayCalleTween = LeanTween.delayedCall(2, StateMachine.NextState).uniqueId;
        }
    }
}