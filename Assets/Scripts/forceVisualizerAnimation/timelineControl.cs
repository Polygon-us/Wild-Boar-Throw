using System;
using UnityEngine;
using UnityEngine.Playables;

public class tiemlineControl : MonoBehaviour
{
    private PlayableDirector playableDirector;
    [SerializeField]
    private ThrowManager throwManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        
        throwManager.OnForceReleased += OnRelease;
        throwManager.OnReset += ResetThrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (!throwManager.isReleased)
        {
            MovePlayableDirector(throwManager.Force / throwManager.ForceController.MaxForce);
        }
    }

    void OnRelease((float force, float angle) args)
    {
        throwManager.isReleased = true;
        MovePlayableDirector(0);
    }

    private void ResetThrow()
    {
        MovePlayableDirector(1.0f);
        
        throwManager.isReleased = false;
    }
    
    private void MovePlayableDirector(float time)
    {
        playableDirector.time = throwManager.ForceController.ChargeCurve.Evaluate(time);
        playableDirector.Evaluate();
    }

}
