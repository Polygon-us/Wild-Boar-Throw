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
    
    private LTDescr delayedCall;

    public float BoarDistance => boar.transform.position.z - initialPosition.position.z;
    
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
        delayedCall = LeanTween.delayedCall(2, OnCollision);
    }

    private void Awake()
    {
        initialPositions = boar.BoarRbs.Select(x => x.position - boar.Parent.position).ToList();
        initialRotations = boar.BoarRbs.Select(x => x.rotation).ToList();
    }

    private void OnThrowBoar((float force, float angle) args)
    {
        boar.MainRb.isKinematic = false;

        Vector3 direction = Quaternion.Euler(-args.angle, 0f, 0f) * initialPosition.forward;
        
        foreach (var rb in boar.BoarRbs)
        {
            rb.AddForce(args.force/boar.BoarRbs.Count * direction, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (!throwManager.isReleased)
        {
            MoveRbToInitialPosition(boar.BoarRbs[0], initialPositions[0], initialRotations[0]);
        }
    }

    private void OnReset()
    {
        if (delayedCall != null)
            LeanTween.cancel(delayedCall.uniqueId);
        
        for (int i = 0; i < boar.BoarRbs.Count; i++)
        {
            MoveRbToInitialPosition(boar.BoarRbs[i], initialPositions[i], initialRotations[i]);
        }
    }

    private void MoveRbToInitialPosition(Rigidbody rb, Vector3 position, Quaternion rotation)
    {
        rb.isKinematic = true;
            
        rb.position = position + initialPosition.position;
        rb.rotation = rotation * initialPosition.rotation; 
            
        rb.isKinematic = rb == boar.MainRb;
    }
}