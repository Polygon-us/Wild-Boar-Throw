using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AngleController : MonoBehaviour
{
    [SerializeField] private float minAngle = 30f;
    [SerializeField] private float maxAngle = 60f;
    [SerializeField] private float anglePingPongTime = 1f;
    [SerializeField] private int pingPongCount = 1;

    [SerializeField] private ThrowManager manager;
    
    [Header("Slider")]
    [SerializeField] private Slider angleSlider;
    [SerializeField] private TMP_Text minAngleText;
    [SerializeField] private TMP_Text maxAngleText;
    [SerializeField] private TMP_Text angleText;

    private Color _originalColor;
    
    public float MinAngle => minAngle;
    public float MaxAngle => maxAngle;
    public float AnglePingPongTime => anglePingPongTime;
    public int PingPongCount => pingPongCount;
    
    public Slider AngleSlider => angleSlider;

    private void OnEnable()
    {
        angleSlider.onValueChanged.AddListener(OnAngleChanged);
    }
    
    private void OnDisable()
    {
        angleSlider.onValueChanged.RemoveListener(OnAngleChanged);
    }
   
    private void Awake()
    {
        angleSlider.minValue = minAngle;
        angleSlider.maxValue = maxAngle;
        angleSlider.value = minAngle;
        
        minAngleText.text = $"{minAngle}°";
        maxAngleText.text = $"{maxAngle}°";
        
        angleText.text = $"{(int)angleSlider.value}°";
        
        _originalColor = angleText.color;
    }
     
    private void OnAngleChanged(float value)
    {
        angleText.text = $"{(int)value}°";
    }

    public void Reset()
    {
        angleSlider.value = minAngle;
        
        angleText.color = _originalColor;
        angleText.text = $"{(int)angleSlider.value}°";
    }
    
    public void Blunder()
    {
        angleText.color = Color.red;
        angleText.text = "BLUNDER";
    }

}
