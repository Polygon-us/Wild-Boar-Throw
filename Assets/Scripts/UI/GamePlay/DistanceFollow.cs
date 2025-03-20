using Gameplay.ThrowStates;
using UnityEngine.UI;
using UnityEngine;
using Gameplay;
using TMPro;

namespace UI.Gameplay
{
    public class DistanceFollow : MonoBehaviour
    {
        [SerializeField] private Transform boar;

        [SerializeField] private Vector3 offset = new(0, 2, 0);
        [SerializeField] private float lerpMultiplier = 10;
        [SerializeField] private float snapLimit = 350;

        [SerializeField] private TMP_Text distanceText;

        [SerializeField] private ThrowManager throwManager;
        [SerializeField] private BoarThrower boarThrower;

        private RectTransform _rectRoot;

        private Vector3 _referenceResolution;
        private Vector3 _screenPos;

        private float _distanceToPos;
        private float _initialDistance;
        private float _currentDistance;

        private bool _wasThrown;

        private Camera _mainCamera;

        private Camera MainCamera
        {
            get
            {
                if (!_mainCamera)
                    _mainCamera = Camera.main;

                return _mainCamera;
            }
        }

        private void Awake()
        {
            _rectRoot = transform as RectTransform;

            _referenceResolution = GetComponentInParent<CanvasScaler>().referenceResolution;
        }

        private void Start()
        {
            _initialDistance = Vector3.Distance(MainCamera.transform.position, boar.position);

            _currentDistance = _initialDistance;
        }

        public void EnableDistanceText(bool enable)
        {
            _wasThrown = enable;
            distanceText.gameObject.SetActive(enable);
        }

        public void Reset()
        {
            _wasThrown = false;
            EnableDistanceText(false);
            distanceText.text = "0 m";
        }

        private void Update()
        {
            if (_wasThrown)
                distanceText.text = $"{boarThrower.BoarDistance:N1} m";
        }

        private void FixedUpdate()
        {
            if (!_wasThrown)
                return;
            
            if (boar)
            {
                _currentDistance = Vector3.Distance(MainCamera.transform.position, boar.position);

                Vector3 screenPos = MainCamera.WorldToScreenPoint(boar.position);
                screenPos.x *= _referenceResolution.x / Screen.width;
                screenPos.y *= _referenceResolution.y / Screen.height;
                _screenPos = screenPos +
                             offset * _initialDistance / _currentDistance;
            }

            _distanceToPos = Vector2.Distance(_rectRoot.anchoredPosition, _screenPos);

            if (_distanceToPos > snapLimit)
                _rectRoot.anchoredPosition = _screenPos;
            else
                _rectRoot.anchoredPosition = Vector2.Lerp(_rectRoot.anchoredPosition, _screenPos,
                    Time.fixedDeltaTime * lerpMultiplier);
        }
    }
}