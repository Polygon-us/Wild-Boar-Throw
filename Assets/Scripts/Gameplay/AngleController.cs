using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AngleController : MonoBehaviour
{
    [SerializeField] private float minAngle = 30f;
    [SerializeField] private float maxAngle = 60f;
    [SerializeField] private float anglePingPongTime = 1f;

    [Header("Slider")]
    [SerializeField] private Slider angleSlider;
    [SerializeField] private TMP_Text minAngleText;
    [SerializeField] private TMP_Text maxAngleText;
    [SerializeField] private TMP_Text angleText;

    public float MinAngle => minAngle;
    public float MaxAngle => maxAngle;
    public float AnglePingPongTime => anglePingPongTime;
    
    public Slider AngleSlider => angleSlider;

    private void OnEnable()
    {
        angleSlider.onValueChanged.AddListener(OnAngleChanged);
    }
    
    private void OnDisable()
    {
        angleSlider.onValueChanged.AddListener(OnAngleChanged);
    }
   
    private void Awake()
    {
        angleSlider.minValue = minAngle;
        angleSlider.maxValue = maxAngle;
        angleSlider.value = minAngle;
        
        minAngleText.text = $"{minAngle}°";
        maxAngleText.text = $"{maxAngle}°";
        
        angleText.text = $"{(int)angleSlider.value}°";
    }
     
    private void OnAngleChanged(float value)
    {
        angleText.text = $"{(int)value}°";
    }

}
