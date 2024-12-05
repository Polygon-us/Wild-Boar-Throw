using System;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    
    [Header("Debug")] 
    [SerializeField] private float force;
    [SerializeField] private float angle;
    [SerializeField] private float simulationTimeScale = 1.0f;
    
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
        stateMachine.OnClick();
    }

    public void NextState()
    {
        stateMachine.NextState();
    }

    public void ResetThrow()
    {
        OnReset?.Invoke();

        stateMachine.OnReset();
        
        // Debug
        Time.timeScale = 1;
    }

    public void Release()
    {
        // Debug
        Time.timeScale = simulationTimeScale;
        
        OnForceReleased?.Invoke((Force, Angle));
    }
}