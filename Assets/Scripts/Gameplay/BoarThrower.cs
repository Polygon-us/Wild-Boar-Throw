using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BoarThrower : MonoBehaviour
{
    [SerializeField] private Transform initialPosition;
    [SerializeField] private Boar boar;

    [FormerlySerializedAs("forceController")] [SerializeField] private ThrowController throwController;
    
    private Rigidbody boarRb;

    public Action OnCollision;
    
    private void OnEnable()
    {
        throwController.OnForceReleased += OnThrowBoar;
        throwController.OnReset += OnReset;

        boar.OnCollision += CallOnCollision;
    }

    private void OnDisable()
    {
        throwController.OnForceReleased -= OnThrowBoar;
        throwController.OnReset -= OnReset;

        boar.OnCollision -= CallOnCollision;
    }
        
    private void Awake()
    {
        boarRb = boar.GetComponent<Rigidbody>();
    }

    private void CallOnCollision()
    {
        OnCollision?.Invoke();
    }

    private void OnThrowBoar((float force, float angle) args)
    {
        boarRb.isKinematic = false;
        
        Vector3 direction = Quaternion.Euler(-args.angle, 0f, 0f) * initialPosition.forward;
        
        boarRb.AddForce(args.force * direction, ForceMode.Impulse);
    }
    
    private void OnReset()
    {
        boar.transform.position = initialPosition.position;
        boar.transform.rotation = initialPosition.rotation;

        boarRb.isKinematic = true;
    }
}