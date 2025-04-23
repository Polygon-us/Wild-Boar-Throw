using Gameplay.ThrowStates;
using UnityEngine;
using TMPro;
using UI.Generic;

namespace Gameplay.Controllers
{
    public class AngleController : MonoBehaviour
    {
        [SerializeField] private float minAngle = 30f;
        [SerializeField] private float maxAngle = 60f;
        
        [SerializeField] private ThrowManager manager;

        [Header("Slider")] 
        [SerializeField] private RectTransform panel;
        [SerializeField] private RectTransform arrowRoot;
        [SerializeField] private TMP_Text minAngleText;
        [SerializeField] private TMP_Text maxAngleText;
        [SerializeField] private TMP_Text angleText;
        [SerializeField] private TweenParams angleParams;
        [SerializeField] private TweenParams showParams;
        
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

        private int _tweenId;
        private Vector3 _startPos;

        private void Awake()
        {
            minAngleText.text = $"{minAngle}°";
            maxAngleText.text = $"{maxAngle}°";

            Angle = minAngle;

            _originalColor = angleText.color;
            _startPos = panel.anchoredPosition;
            Hide(true);
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
                    angleParams.duration)
                .setOnUpdate(t => Angle = t)
                .setLoopPingPong().uniqueId;
        }
        
        public void Show()
        {
            _tweenId = LeanTween.move(panel, _startPos, showParams.duration)
                .setEase(showParams.inType)
                .uniqueId;
            AudioManager.Instance.PlayUI("Counter");
        }

        public void Hide(bool instant = false)
        {
            Vector3 newPos = _startPos + new Vector3(panel.sizeDelta.x, -panel.sizeDelta.y);
            _tweenId = LeanTween.move(panel, newPos, instant ? 0 : showParams.duration)
                .setEase(showParams.outType)
                .uniqueId;
        }
        
        public void StopTween()
        {
            LeanTween.cancel(_tween);
        }
    }
}