using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

public class CrowdController : MonoBehaviour
{
    [SerializeField] private Material crowdMaterial;
    [SerializeField] [Range(5, 70)] private float waveSpeedRange = 70;
    [SerializeField] [Range(0.02f, 0.04f)] private float waveAmplitudeRange = 0.04f;

    public void MakeImpression(float amount)
    {
        crowdMaterial.SetFloat("_waveSpeed", math.clamp(waveSpeedRange * amount, 5, 70));
        crowdMaterial.SetFloat("_waveAmplitude", math.clamp(waveAmplitudeRange * amount, 0.02f, 0.04f));
    }
}