using Unity.Cinemachine;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera standingCamera;
    [SerializeField] private CinemachineCamera followCamera;
    [SerializeField] private ForceController forceController;

    private void OnEnable()
    {
        forceController.OnForceReleased += SwitchCamera;
        forceController.OnReset += Reset;
    }
    
    private void OnDisable()
    {
        forceController.OnForceReleased -= SwitchCamera;
        forceController.OnReset -= Reset;
    }

    private void SwitchCamera(float force, float _)
    {
        if (force > 0f)
        {
            followCamera.Priority = 10;
            standingCamera.Priority = 0;
        }
        else
        {
            followCamera.Priority = 0;
            standingCamera.Priority = 10;
        }
    }
    
    private void Reset()
    {
        followCamera.Priority = 0;
        standingCamera.Priority = 10;
    }
}
