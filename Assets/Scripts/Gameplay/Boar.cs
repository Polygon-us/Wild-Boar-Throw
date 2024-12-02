using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class Boar : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private Rigidbody boarRb;
    [SerializeField] private LayerMask layerMask;
    
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
    public Transform Parent => parent;
    
    public Action OnCollision;


    private void OnTriggerEnter(Collider other)
    {
        if (!layerMask.IsInLayerMask(other.gameObject.layer))
            return;
        
        OnCollision?.Invoke();
    }
}
