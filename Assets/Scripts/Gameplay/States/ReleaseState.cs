using ForceVisualizerAnimation;
using Gameplay.Controllers;
using UI.Gameplay;
using UnityEngine;

namespace Gameplay.States
{
    public class ReleaseState : StateBase
    {
        [SerializeField] private ThrowManager throwManager;
        [SerializeField] private DistanceFollowView distanceFollowView;
        [SerializeField] private BoarThrower boarThrower;
        [SerializeField] private ForceVisualizerController forceVisualizerController;
        [SerializeField] private Boar boar;

        private int _delayCallTween;

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

            LeanTween.cancel(_delayCallTween);
        }

        private void CallOnCollision()
        {
            boar.OnCollision -= CallOnCollision;
            AudioManager.Instance.PlaySFX("PigFall", 1f);

            _delayCallTween = LeanTween.delayedCall(2, StateMachine.NextState).uniqueId;
        }
    }
}