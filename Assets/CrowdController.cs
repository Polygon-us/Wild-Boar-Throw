using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

public class CrowdController : MonoBehaviour
{
    [SerializeField] private Material crowdMaterial;
    private ForceController forceController;
    [SerializeField][Range(0,1)] private float force;

    [SerializeField][Range(1, 70)] private float waveSpeedRange = 60;

    public void MakeImpression(float amount)
    {
        crowdMaterial.SetFloat("_waveSpeed", waveSpeedRange * math.clamp(amount, 0, 1));
    }
}