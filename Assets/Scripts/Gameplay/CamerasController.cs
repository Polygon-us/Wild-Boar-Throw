using Unity.Cinemachine;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera standingCamera;
    [SerializeField] private CinemachineCamera followCamera;
    [SerializeField] private CinemachineCamera landingCamera;
    [SerializeField] private ThrowManager throwManager;
    [SerializeField] private BoarThrower boarThrower;
    
    private CinemachineCamera currentCamera;
    
    private void OnEnable()
    {
        throwManager.OnForceReleased += OnThrow;
        throwManager.OnReset += OnReset;

        boarThrower.OnCollision += ShowLanding;
    }

    private void OnDisable()
    {
        throwManager.OnForceReleased -= OnThrow;
        throwManager.OnReset -= OnReset;
        
        boarThrower.OnCollision -= ShowLanding;
    }

    private void OnThrow((float, float) _)
    {
        ChangeCamera(followCamera);
    }

    private void OnReset()
    {
        ChangeCamera(standingCamera);
    }

    private void ShowLanding()
    {
        ChangeCamera(landingCamera);
    }

    private void ChangeCamera(CinemachineCamera newCamera)
    {
        if (currentCamera)
            currentCamera.Priority = 0;
        
        currentCamera = newCamera;
        
        currentCamera.Priority = 10;
    }
}