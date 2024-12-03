using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class BoarThrower : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Boar boar;

    [SerializeField] private ThrowManager throwManager;

    public Action OnCollision;

    // Stores the character's RBs relative positions and rotations to the body, we use this to maintain the body shape when we teleport it around.
    private List<Vector3> relativeRbPositions;
    private List<Quaternion> relativeRbRotations;
    
    private LTDescr delayedCall;

    public float BoarDistance => boar.transform.position.z - startPosition.position.z;
    
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
        relativeRbPositions = boar.BoarRbs.Select(x => x.position - boar.Parent.position).ToList();
        relativeRbRotations = boar.BoarRbs.Select(x => x.rotation).ToList();
    }

    private void OnThrowBoar((float force, float angle) args)
    {
        boar.MainRb.isKinematic = false;

        Vector3 direction = Quaternion.Euler(-args.angle, 0f, 0f) * startPosition.forward;
        
        foreach (var rb in boar.BoarRbs)
        {
            rb.AddForce(args.force/boar.BoarRbs.Count * direction, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (!throwManager.isReleased)
        {
            MoveRbToStartPosition(boar.BoarRbs[0], relativeRbPositions[0], relativeRbRotations[0]);
        }
    }

    private void OnReset()
    {
        if (delayedCall != null)
            LeanTween.cancel(delayedCall.uniqueId);
        
        for (int i = 0; i < boar.BoarRbs.Count; i++)
        {
            MoveRbToStartPosition(boar.BoarRbs[i], relativeRbPositions[i], relativeRbRotations[i]);
        }
    }

    private void MoveRbToStartPosition(Rigidbody rb, Vector3 relativePosition, Quaternion relativeRotation)
    {
        rb.isKinematic = true;
            
        rb.position = relativePosition + startPosition.position;
        rb.rotation = relativeRotation * startPosition.rotation; 
            
        rb.isKinematic = rb == boar.MainRb;
    }
}