using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera standingCamera;
    [SerializeField] private CinemachineCamera followCamera;
    [SerializeField] private CinemachineCamera landingCamera;
    [FormerlySerializedAs("forceController")] [SerializeField] private ThrowController throwController;
    [SerializeField] private BoarThrower boarThrower;
    
    private void OnEnable()
    {
        throwController.OnForceReleased += OnThrow;
        throwController.OnReset += OnReset;

        boarThrower.OnCollision += ShowLanding;
    }

    private void OnDisable()
    {
        throwController.OnForceReleased -= OnThrow;
        throwController.OnReset -= OnReset;
        
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