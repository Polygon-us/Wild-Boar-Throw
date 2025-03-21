using Unity.Cinemachine;
using UnityEngine;

namespace Gameplay.Controllers
{
    public class CamerasController : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera standingCamera;
        [SerializeField] private CinemachineCamera followCamera;
        [SerializeField] private CinemachineCamera landingCamera;

        private CinemachineCamera currentCamera;
        public CinemachineCamera CurrentCamera => currentCamera;

        public void FollowCamera()
        {
            ChangeCamera(followCamera);
        }

        public void Reset()
        {
            ResetCameras();

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

        private void ResetCameras()
        {
            standingCamera.Priority = 0;
            followCamera.Priority = 0;
            landingCamera.Priority = 0;
        }
    }
}