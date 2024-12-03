using System;
using UnityEngine;
using UnityEngine.Playables;

public class tiemlineControl : MonoBehaviour
{
    private PlayableDirector playableDirector;
    [SerializeField]
    private ThrowManager throwManager;

    private bool isReleased;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        isReleased = false;
        throwManager.OnForceReleased += OnRelease;
        throwManager.OnReset += ResetThrow;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReleased)
        {
            MovePlayableDirector(throwManager.Force / throwManager.ForceController.MaxForce);
        }
    }

    void OnRelease((float force, float angle) args)
    {
        isReleased = true;
        MovePlayableDirector(0);
    }

    private void ResetThrow()
    {
        MovePlayableDirector(1.0f);
        isReleased = false;
    }
    
    private void MovePlayableDirector(float time)
    {
        playableDirector.time = throwManager.ForceController.ChargeCurve.Evaluate(time);
        playableDirector.Evaluate();
    }

}
