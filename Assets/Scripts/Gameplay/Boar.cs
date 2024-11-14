using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boar : MonoBehaviour
{
    [SerializeField] private Rigidbody boarRb;
    
    private List<Rigidbody> boarRbs;

    public List<Rigidbody> BoarRbs
    {
        get
        {
            if (boarRbs == null || boarRbs.Count == 0) 
                boarRbs = GetComponentsInChildren<Rigidbody>().ToList();    
            
            return boarRbs;
        }
    }
    
    public Rigidbody MainRb => boarRb;
    
    public Action OnCollision;

    private void OnCollisionEnter(Collision _)
    {
        OnCollision?.Invoke();
    }
}
