using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void OnEnable()
    {
        throwManager.OnForceReleased += OnThrowBoar;
        throwManager.OnReset += OnReset;
    }

    private void OnDisable()
    {
        throwManager.OnForceReleased -= OnThrowBoar;
        throwManager.OnReset -= OnReset;
    }

    private void Awake()
    {
        _rectRoot = transform as RectTransform;

        CanvasScaler canvasScaler = GetComponentInParent<CanvasScaler>();
        canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
    }

    private void Start()
    {
        _initialDistance = Vector3.Distance(MainCamera.transform.position, boar.position);
        
        _currentDistance = _initialDistance;
    }

    private void OnThrowBoar((float force, float angle) obj)
    {
        _wasThrown = true;
        
        distanceText.gameObject.SetActive(true);
    }

    private void OnReset()
    {
        _wasThrown = false;
        
        distanceText.text = "0 m";
        distanceText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_wasThrown)
            distanceText.text = $"{boarThrower.BoarDistance:N1} m";
    }

    private void FixedUpdate()
    {
        if (boar)
        {
            _currentDistance = Vector3.Distance(MainCamera.transform.position, boar.position);
            
            _screenPos = MainCamera.WorldToScreenPoint(boar.position) + offset * _initialDistance / _currentDistance;
        }
        
        _distanceToPos = Vector2.Distance(_rectRoot.anchoredPosition, _screenPos);

        if (_distanceToPos > snapLimit)
            _rectRoot.anchoredPosition = _screenPos;
        else
            _rectRoot.anchoredPosition = Vector2.Lerp(_rectRoot.anchoredPosition, _screenPos, Time.fixedDeltaTime * lerpMultiplier);
    }
}