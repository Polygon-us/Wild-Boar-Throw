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

    public void FollowCamera()
    {
        ChangeCamera(followCamera);
    }

    public void Reset()
    {
        ChangeCamera(standingCamera);
    }

    public void ShowLanding()
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