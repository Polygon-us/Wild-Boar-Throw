using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoarThrower : MonoBehaviour
{
    [SerializeField] private Transform initialPosition;
    [SerializeField] private Boar boar;

    [SerializeField] private ThrowManager throwManager;

    public Action OnCollision;

    private List<Vector3> initialPositions;
    private List<Quaternion> initialRotations;

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

    private void Awake()
    {
        initialPositions = boar.BoarRbs.Select(x => x.position - boar.transform.position).ToList();
        initialRotations = boar.BoarRbs.Select(x => x.rotation).ToList();
    }

    private void OnThrowBoar((float force, float angle) args)
    {
        boar.MainRb.isKinematic = false;

        Vector3 direction = Quaternion.Euler(-args.angle, 0f, 0f) * initialPosition.forward;

        boar.MainRb.AddForce(args.force * direction, ForceMode.Impulse);
    }

    private void OnReset()
    {
        for (int i = 0; i < boar.BoarRbs.Count; i++)
        {
            Rigidbody rb = boar.BoarRbs[i];

            rb.isKinematic = true;
            
            rb.position = initialPositions[i] + initialPosition.position;
            rb.rotation = initialRotations[i] * initialPosition.rotation; 
            
            rb.isKinematic = rb == boar.MainRb;
        }
    }
}