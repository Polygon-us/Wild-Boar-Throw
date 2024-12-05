using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    protected StateMachine StateMachine { get; private set; }
    
    public virtual void OnEnterState(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public virtual void OnExitState()
    {
    }

    public virtual void OnUpdate()
    {
    }

    public virtual void OnClick()
    {
    }

    public virtual void OnReset()
    {
    }
}