using Gameplay.ThrowStates;
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
        [SerializeField] private RectTransform arrowRoot;
        [SerializeField] private TMP_Text minAngleText;
        [SerializeField] private TMP_Text maxAngleText;
        [SerializeField] private TMP_Text angleText;
        
        public float Angle
        {
            get => _angle;
            private set
            {
                float percentage = Mathf.InverseLerp(maxAngle, minAngle, value);
                float rotation = Mathf.Lerp(0, 90, percentage);
                arrowRoot.rotation = Quaternion.Euler(0, 0, rotation);
                
                angleText.text = $"{(int)value}°";
                
                _angle = value;
            }
        }

        private float _angle;
        private Color _originalColor;
        private int _tween;

        
        private void Awake()
        {
            minAngleText.text = $"{minAngle}°";
            maxAngleText.text = $"{maxAngle}°";

            Angle = minAngle;

            _originalColor = angleText.color;
        }
        
        public void Reset()
        {
            Angle = minAngle;

            angleText.color = _originalColor;
        }

        public void Blunder()
        {
            Angle = minAngle;
            
            angleText.color = Color.red;
            angleText.text = "BLUNDER";
        }

        public void StartTween()
        {
            _tween = LeanTween.value(minAngle, maxAngle,
                    anglePingPongTime)
                .setOnUpdate(t => Angle = t)
                .setLoopPingPong().uniqueId;
        }

        public void StopTween()
        {
            LeanTween.cancel(_tween);
        }
    }
}