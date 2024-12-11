using Unity.Cinemachine;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera standingCamera;
    [SerializeField] private CinemachineCamera followCamera;
    [SerializeField] private CinemachineCamera landingCamera;
    [SerializeField] private ThrowManager throwManager;
    [SerializeField] private BoarThrower boarThrower;
    [SerializeField] private Transform hideOnLanding;
    
    private CinemachineCamera currentCamera;
    
    private void OnEnable()
    {
        boarThrower.OnCollision += ShowLanding;
    }

    private void OnDisable()
    {
        boarThrower.OnCollision -= ShowLanding;
    }

    public void FollowCamera()
    {
        ChangeCamera(followCamera);
    }

    public void Reset()
    {
        ChangeCamera(standingCamera);
        // TODO: this should not be implemented here, is a quick experiment, try moving it to its own class with subscription to the OnCollision event
        hideOnLanding.localPosition = new Vector3(14.7f, 0, 0);
        hideOnLanding.eulerAngles = new Vector3(0, 0, 0);
    }

    private void ShowLanding()
    {
        ChangeCamera(landingCamera);
        // TODO: this should not be implemented here, is a quick experiment, try moving it to its own class with subscription to the OnCollision event
        hideOnLanding.localPosition = new Vector3(-13, 11, 0);
        hideOnLanding.eulerAngles = new Vector3(0, 0, 36);
    }

    private void ChangeCamera(CinemachineCamera newCamera)
    {
        if (currentCamera)
            currentCamera.Priority = 0;
        
        currentCamera = newCamera;
        
        currentCamera.Priority = 10;
    }
}