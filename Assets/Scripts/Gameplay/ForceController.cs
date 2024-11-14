using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForceController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float maxForce = 100f;
    [SerializeField, Range(0, 1)] private float incrementPercentage = 0.1f;
    [SerializeField, Range(0, 1)] private float decrementPercentage = 0.1f;
    [SerializeField] private float forceChargeTime = 3;
    [SerializeField] private AnimationCurve chargeCurve;
    
    [Header("Slider")]
    [SerializeField] private Slider forceSlider;
    [SerializeField] private Slider timerSlider;
    [SerializeField] private TMP_Text stateText;
    
    public float MaxForce => maxForce;
    public float IncrementPercentage => incrementPercentage;
    public float DecrementPercentage => decrementPercentage;
    public float ForceChargeTime => forceChargeTime;
    
    public AnimationCurve ChargeCurve => chargeCurve;
    
    public Slider ForceSlider => forceSlider;
    public Slider TimerSlider => timerSlider;
    
    public TMP_Text StateText => stateText;
}
