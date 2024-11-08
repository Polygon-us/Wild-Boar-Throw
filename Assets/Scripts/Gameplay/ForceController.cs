using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForceController : MonoBehaviour
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
    [SerializeField] private ControlType controlType;

    public Action<float, float> OnForceReleased;
    public Action OnReset;
    
    private void Start()
    {
        controlType = ControlType.Charging;
        forceSlider.maxValue = maxForce;
        forceSlider.minValue = 0f;
        
        Reset();
    }

    private void OnDisable()
    {
        TapClickController.OnClick -= OnClick;
    }

    public void Reset()
    {
        controlType = ControlType.Charging;

        numClicks = 0;
        force = 0f;
        chargeTimer = 0f;
        forceSlider.value = 0f;
            
        stateText.text = $"{controlType.ToString()}\n{numClicks} clicks";
            
        TapClickController.OnClick += OnClick;
        
        OnReset?.Invoke();
    }
    
    private void OnClick()
    {
        numClicks++;
        
        float forceResistance = chargeCurve.Evaluate(force / maxForce);
        
        UpdateForce(forceResistance * maxForce * incrementPercentage);
        
        stateText.text = $"{controlType.ToString()}\n{numClicks} clicks";
    }
    
    private void Update()
    {
        if (controlType == ControlType.Charging)
        {
            chargeTimer += Time.deltaTime;
            
            UpdateForce(- maxForce * decrementPercentage * Time.deltaTime);
        }
        
        if (chargeTimer >= chargeTime)
        {
            Release();
            chargeTimer = 0f;
        }
    }
    
    private void Release()
    {
        if (controlType != ControlType.Charging) 
            return;
        
        controlType = ControlType.Releasing;
            
        TapClickController.OnClick -= OnClick;
            
        stateText.text = $"{controlType.ToString()}\n{numClicks} clicks\n{force} N";
            
        OnForceReleased?.Invoke(force, angleController.Angle);
    }

    private void UpdateForce(float delta)
    {
        force = Mathf.Clamp(force + delta, 0f, maxForce);
        
        forceSlider.value = force;
    }
}

public enum ControlType
{
    Charging,
    Releasing
}