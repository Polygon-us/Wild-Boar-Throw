using Unity.Cinemachine;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera standingCamera;
    [SerializeField] private CinemachineCamera followCamera;
    [SerializeField] private CinemachineCamera landingCamera;
    [SerializeField] private ForceController forceController;
    [SerializeField] private BoarThrower boarThrower;
    
    private void OnEnable()
    {
        forceController.OnForceReleased += OnThrow;
        forceController.OnReset += OnReset;

        boarThrower.OnCollision += ShowLanding;
    }

    private void OnDisable()
    {
        forceController.OnForceReleased -= OnThrow;
        forceController.OnReset -= OnReset;
        
        boarThrower.OnCollision -= ShowLanding;
    }

    private void OnThrow((float, float) _)
    {
        
        followCamera.Priority = 10;
        standingCamera.Priority = 0;
        landingCamera.Priority = 0;
    }

    private void OnReset()
    {
        followCamera.Priority = 0;
        standingCamera.Priority = 10;
        landingCamera.Priority = 0;
    }

    private void ShowLanding()
    {
        followCamera.Priority = 0;
        standingCamera.Priority = 0;
        landingCamera.Priority = 10;
    }
}