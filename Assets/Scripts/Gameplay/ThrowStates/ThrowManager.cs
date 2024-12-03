using System;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    [SerializeField] private ForceController forceController;
    [SerializeField] private AngleController angleController;
    [SerializeField] private CountController countController;
    
    [Header("Debug")] 
    [SerializeField] private float force;
    [SerializeField] private float angle;
    [SerializeField] private float simulationTimeScale = 1.0f;
    
    // TODO: is there a way to handle this another way? knowing that we have a state machine...
    // We will need to change the dependency chain a bit.
    public bool isReleased;

    private IThrowState currentState;

    public ForceController ForceController => forceController;
    public AngleController AngleController => angleController;
    public CountController CountController => countController;

    public float Force { get => force; set => force = value; }
    public float Angle { get => angle; set => angle = value; }

    public Action<(float force, float angle)> OnForceReleased;
    public Action OnReset;

    private void Start()
    {
        ResetThrow();
    }

    private void OnEnable()
    {
        TapClickController.OnClick += OnClick;
    }

    private void OnDisable()
    {
        TapClickController.OnClick -= OnClick;
    }

    private void OnClick()
    {
        currentState.OnClick();
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }

    public void ChangeState(IThrowState newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState.OnEnterState(this);
    }

    public void ResetThrow()
    {
        OnReset?.Invoke();
        
        isReleased = false;

        ChangeState(new ForceState());

        force = 0;
        
        // Debug
        Time.timeScale = 1;
    }

    public void Release()
    {
        // Debug
        Time.timeScale = simulationTimeScale;
        
        isReleased = true;
        
        OnForceReleased?.Invoke((Force, Angle));
    }
}