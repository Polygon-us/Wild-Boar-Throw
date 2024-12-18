using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class BoarThrower : MonoBehaviour
{
    [SerializeField] private Transform startingPoint;
    [SerializeField] private Boar boar;

    public Action OnCollision;

    private List<Vector3> initialPositions;
    private List<Quaternion> initialRotations;
    
    private LTDescr delayedCall;
    
    public float BoarDistance => boar.transform.position.z - startingPoint.position.z;

    private void CallOnCollision()
    {
        delayedCall = LeanTween.delayedCall(2, OnCollision);
    }

    private void Awake()
    {
        initialPositions = boar.BoarRbs.Select(x => x.position - boar.Parent.position).ToList();
        initialRotations = boar.BoarRbs.Select(x => x.rotation).ToList();
    }

    public void ThrowBoar(float force, float angle)
    {
        boar.MainRb.isKinematic = false;

        Vector3 direction = Quaternion.Euler(-angle, 0f, 0f) * startingPoint.forward;
        
        foreach (var rb in boar.BoarRbs)
        {
            rb.AddForce(force/boar.BoarRbs.Count * direction, ForceMode.Impulse);
        }
    }
    
    public void MoveBoarWithStartingPosition()
    {
        MoveRbToStartingPosition(boar.BoarRbs[0], initialPositions[0], initialRotations[0]);
    }
    
    public void Reset()
    {
        if (delayedCall != null)
            LeanTween.cancel(delayedCall.uniqueId);
        
        for (int i = 0; i < boar.BoarRbs.Count; i++)
        {
            MoveRbToStartingPosition(boar.BoarRbs[i], initialPositions[i], initialRotations[i]);
        }
    }
    
    private void MoveRbToStartingPosition(Rigidbody rb, Vector3 position, Quaternion rotation)
    {
        rb.isKinematic = true;
            
        rb.position = position + startingPoint.position;
        rb.rotation = rotation * startingPoint.rotation; 
            
        rb.isKinematic = rb == boar.MainRb;

    }
}