using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThrowController : MonoBehaviour
{
    [SerializeField] private float maxForce = 100f;
    [SerializeField, Range(0, 1)] private float incrementPercentage = 0.1f;
    [SerializeField, Range(0, 1)] private float decrementPercentage = 0.1f;
    [SerializeField] private float chargeTime = 3;
    [SerializeField] private AnimationCurve chargeCurve;
    [SerializeField] private AngleController angleController;

    [Header("UI")]
    [SerializeField] private Slider forceSlider;
    [SerializeField] private TMP_Text stateText;

    [Header("Debug")]
    [SerializeField] private float force;
    [SerializeField] private float chargeTimer;
    [SerializeField] private int numClicks;
    
    private IThrowState currentState;
    
    public Action<(float force, float angle)> OnForceReleased;
    public Action OnReset;
    
    public float MaxForce => maxForce;
    public float IncrementPercentage => incrementPercentage;
    public float DecrementPercentage => decrementPercentage;
    public float ChargeTime => chargeTime;
    public AnimationCurve ChargeCurve => chargeCurve;
    public AngleController AngleController => angleController;
    public Slider ForceSlider => forceSlider;
    public TMP_Text StateText => stateText;
    public float Force { get => force; set => force = value; }
    

    private void Start()
    {
        ChangeState(new ForceState());
        
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
    
    public void ChangeState(IThrowState newState)
    {
        currentState?.OnExitState();
        currentState = newState;
        currentState.OnEnterState(this);
    }
    
    public void ResetThrow()
    {
        OnReset?.Invoke();
        
        ChangeState(new ForceState());
    }

    public void Release()
    {
        OnForceReleased?.Invoke((force, angleController.Angle));
    }

}
