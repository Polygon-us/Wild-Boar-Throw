using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BoarThrower : MonoBehaviour
{
    [SerializeField] private Transform initialPosition;
    [SerializeField] private Boar boar;

    [SerializeField] private ThrowManager throwManager;

    public Action OnCollision;
    
    private void OnEnable()
    {
        throwManager.OnForceReleased += OnThrowBoar;
        throwManager.OnReset += OnReset;

        boar.OnCollision += CallOnCollision;
    }

    private void OnDisable()
    {
        throwManager.OnForceReleased -= OnThrowBoar;
        throwManager.OnReset -= OnReset;

        boar.OnCollision -= CallOnCollision;
    }

    private void CallOnCollision()
    {
        OnCollision?.Invoke();
    }

    private void OnThrowBoar((float force, float angle) args)
    {
        boar.BoarRb.isKinematic = false;
        
        Vector3 direction = Quaternion.Euler(-args.angle, 0f, 0f) * initialPosition.forward;
        
        boar.BoarRb.AddForce(args.force * direction, ForceMode.Impulse);
    }
    
    private void OnReset()
    {
        boar.transform.position = initialPosition.position;
        boar.transform.rotation = initialPosition.rotation;

        boar.BoarRb.isKinematic = true;
    }
}