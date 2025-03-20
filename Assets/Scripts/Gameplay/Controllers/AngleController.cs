using Gameplay.ThrowStates;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace Gameplay.Controllers
{
    public class AngleController : MonoBehaviour
    {
        [SerializeField] private float minAngle = 30f;
        [SerializeField] private float maxAngle = 60f;
        [SerializeField] private float anglePingPongTime = 1f;
        [SerializeField] private int angleSelectionTime = 3;
        
        [SerializeField] private ThrowManager manager;

        [Header("Slider")] 
        [SerializeField] private Slider angleSlider;
        [SerializeField] private TMP_Text minAngleText;
        [SerializeField] private TMP_Text maxAngleText;
        [SerializeField] private TMP_Text angleText;
        
        public int AngleSelectionTime => angleSelectionTime;
        public float Angle => angleSlider.value;

        
        private Color _originalColor;
        private int _tween;

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

            angleText.text = $"{(int) angleSlider.value}°";

            _originalColor = angleText.color;
        }

        private void OnAngleChanged(float value)
        {
            angleText.text = $"{(int) value}°";
        }

        public void Reset()
        {
            angleSlider.value = minAngle;

            angleText.color = _originalColor;
            angleText.text = $"{(int) angleSlider.value}°";
        }

        public void Blunder()
        {
            angleSlider.value = minAngle;
            
            angleText.color = Color.red;
            angleText.text = "BLUNDER";
        }

        public void StartTween()
        {
            _tween = LeanTween.value(minAngle, maxAngle,
                    anglePingPongTime)
                .setOnUpdate(t =>
                {
                    angleSlider.value = t;
                })
                .setLoopPingPong().uniqueId;
        }

        public void StopTween()
        {
            LeanTween.cancel(_tween);
        }
    }
}