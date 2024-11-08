using System;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera standingCamera;
    [SerializeField] private CinemachineCamera followCamera;
    [SerializeField] private CinemachineCamera landingCamera;
    [SerializeField] private ForceController forceController;

    private void OnEnable()
    {
        forceController.OnForceReleased += OnThrow;
        forceController.OnReset += OnReset;
    }

    private void OnDisable()
    {
        forceController.OnForceReleased -= OnThrow;
        forceController.OnReset -= OnReset;
    }

    private async void OnThrow(float force, float _)
    {
        followCamera.Priority = 10;
        standingCamera.Priority = 0;
        landingCamera.Priority = 0;
        
        await Task.Delay(TimeSpan.FromSeconds(2));
        
        ShowLanding();
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