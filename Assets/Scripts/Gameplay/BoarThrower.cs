using UnityEngine;

public class BoarThrower : MonoBehaviour
{
    [SerializeField] private Transform initialPosition;
    [SerializeField] private GameObject boar;

    [SerializeField] private ForceController forceController;
    
    private Rigidbody boarRb;

    private void OnEnable()
    {
        forceController.OnForceReleased += OnThrowBoar;
        forceController.OnReset += OnReset;
    }

    private void OnDisable()
    {
        forceController.OnForceReleased -= OnThrowBoar;
        forceController.OnReset -= OnReset;
    }
        
    private void Awake()
    {
        boarRb = boar.GetComponent<Rigidbody>();
    }

    private void OnThrowBoar(float force, float angle)
    {
        boarRb.isKinematic = false;
        
        Vector3 direction = Quaternion.Euler(-angle, 0f, 0f) * initialPosition.forward;
        
        boarRb.AddForce(force * direction, ForceMode.Impulse);
    }
    
    private void OnReset()
    {
        boar.transform.position = initialPosition.position;
        boar.transform.rotation = initialPosition.rotation;

        boarRb.isKinematic = true;
    }
}