using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForceController : MonoBehaviour
{
    [SerializeField] private float maxForce = 100f;
    [SerializeField] private float forceIncrement = 10f;
    [SerializeField] private float forceDecrement = 10f;
    [SerializeField] private float chargeTime = 3;
    [SerializeField] private AnimationCurve chargeCurve;
    
    [Header("UI")]
    [SerializeField] private Slider forceSlider;
    [SerializeField] private TMP_Text stateText;
    
    [Header("Debug")]
    [SerializeField] private float force;
    [SerializeField] private float chargeTimer;
    [SerializeField] private int numClicks;
    [SerializeField] private ControlType controlType;

    private void Start()
    {
        controlType = ControlType.Releasing;
        forceSlider.maxValue = maxForce;
        forceSlider.minValue = 0f;
        
        SwitchState();
    }

    private void OnDisable()
    {
        TapClickController.OnClick -= OnClick;
    }

    private void OnClick()
    {
        numClicks++;
        
        float forceResistance = chargeCurve.Evaluate(force / maxForce);
        
        UpdateForce(forceResistance * forceIncrement);
        
        stateText.text = $"{controlType.ToString()}\n{numClicks} clicks";
    }
    
    private void Update()
    {
        if (controlType == ControlType.Charging)
        {
            chargeTimer += Time.deltaTime;
            
            UpdateForce(-forceDecrement * Time.deltaTime);
        }
        
        if (chargeTimer >= chargeTime)
        {
            SwitchState();
            chargeTimer = 0f;
        }
    }
    
    private async void SwitchState()
    {
        if (controlType == ControlType.Charging)
        {
            controlType = ControlType.Releasing;
            
            TapClickController.OnClick -= OnClick;
            
            stateText.text = $"{controlType.ToString()}\n{numClicks} clicks\n{force} N";
            
            await Task.Delay(TimeSpan.FromSeconds(chargeTime));
            
            if (Application.isPlaying)
                SwitchState();
        }
        else
        {
            controlType = ControlType.Charging;

            numClicks = 0;
            force = 0f;
            forceSlider.value = 0f;
            
            stateText.text = $"{controlType.ToString()}\n{numClicks} clicks";
            
            TapClickController.OnClick += OnClick;
        }
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