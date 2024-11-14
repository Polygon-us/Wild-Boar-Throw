using System;
using UnityEngine;

public class Boar : MonoBehaviour
{
    [SerializeField] private Rigidbody boarRb;
    
    public Rigidbody BoarRb => boarRb;
    
    public Action OnCollision;

    private void OnCollisionEnter(Collision _)
    {
        OnCollision?.Invoke();
    }
}
