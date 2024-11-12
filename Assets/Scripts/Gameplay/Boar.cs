using System;
using UnityEngine;

public class Boar : MonoBehaviour
{
    public Action OnCollision;

    private void OnCollisionEnter(Collision _)
    {
        OnCollision?.Invoke();
    }
}
