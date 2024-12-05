using UnityEngine;

public class ReleaseState : StateBase
{
    [SerializeField] private ThrowManager throwManager;
    
    public override void OnEnterState(StateMachine stateMachine)
    {
        base.OnEnterState(stateMachine);
        
        throwManager.Release();
    }
}