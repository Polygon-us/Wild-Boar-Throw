using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private List<StateBase> states = new();
   
    private StateBase currentState;
    
    public void OnClick()
    {
        currentState.OnClick();
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }

    public void ChangeState(StateBase newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState.OnEnterState(this);
    }
    
    public void NextState()
    {
        if (currentState == states[^1])
            return;
        
        ChangeState(states[states.IndexOf(currentState) + 1]);
    }

    public void OnReset()
    {
        foreach (StateBase state in states)
             state.OnReset();
        
        // currentState?.OnReset();
        
        if (states.Count > 0)
            ChangeState(states[0]);
    }
}
