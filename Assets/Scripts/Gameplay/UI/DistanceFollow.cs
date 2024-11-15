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

    private Camera _mainCamera;
    private RectTransform _rectRoot;

    private Vector3 _screenPos;
    private Vector2 _referenceResolution;

    private float _distance;

    private bool _wasThrown;

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

        _referenceResolution = GetComponentInParent<CanvasScaler>().referenceResolution;

        SetCamera();
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

    private void SetCamera()
    {
        _mainCamera = Camera.main;
    }

    private void NormalizePosition(ref Vector3 pos)
    {
        pos.x = pos.x / Screen.width * _referenceResolution.x;
        pos.y = pos.y / Screen.height * _referenceResolution.y;
    }

    private void Update()
    {
        if (_wasThrown)
            distanceText.text = $"{boarThrower.BoarDistance:N1} m";
    }

    private void FixedUpdate()
    {
        if (!_mainCamera)
            SetCamera();

        if (boar && _mainCamera)
            _screenPos = _mainCamera.WorldToScreenPoint(boar.position + offset);

        NormalizePosition(ref _screenPos);

        _distance = Vector2.Distance(_rectRoot.anchoredPosition, _screenPos);

        if (_distance > snapLimit)
            _rectRoot.anchoredPosition = _screenPos;
        else
            _rectRoot.anchoredPosition = Vector2.Lerp(_rectRoot.anchoredPosition, _screenPos, Time.fixedDeltaTime * lerpMultiplier);
    }
}