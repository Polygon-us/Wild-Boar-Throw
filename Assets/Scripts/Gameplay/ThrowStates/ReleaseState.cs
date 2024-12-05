using System;
using ForceVisualizerAnimation;
using UnityEngine;

public class ReleaseState : StateBase
{
    [SerializeField] private ThrowManager throwManager;
    [SerializeField] private DistanceFollow distanceFollow;
    [SerializeField] private BoarThrower boarThrower;
    [SerializeField] private CamerasController camerasController;
    [SerializeField] private ForceVisualizerController forceVisualizerController;
    
    public override void OnEnterState(StateMachine stateMachine)
    {
        base.OnEnterState(stateMachine);
        
        throwManager.Release();

        distanceFollow.EnableDistanceText(true);
        
        boarThrower.ThrowBoar(throwManager.Force, throwManager.Angle);

        camerasController.FollowCamera();
        
        forceVisualizerController.MovePlayableDirector(0);
    }

    public override void OnReset()
    {
        distanceFollow.Reset();
    }
}