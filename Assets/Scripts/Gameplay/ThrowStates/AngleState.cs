using UnityEngine;

public class AngleState : StateBase
{
    [SerializeField] private AngleController angleController;
    [SerializeField] private ThrowManager throwManager;
    
    private LTDescr pingPongTween;
    private float angle;

    public override void OnEnterState(StateMachine stateMachine)
    {
        base.OnEnterState(stateMachine);
        
        pingPongTween = LeanTween.value(angleController.MinAngle, angleController.MaxAngle, angleController.AnglePingPongTime)
            .setOnUpdate(t =>
            {
                angle = t;
                angleController.AngleSlider.value = angle;
            })
            .setLoopPingPong(angleController.PingPongCount)
            .setOnComplete(Blunder);
    }

    public override void OnExitState()
    {
        LeanTween.cancel(pingPongTween.uniqueId);
    }
    
    public override void OnClick()
    {
        throwManager.Angle = angle;

        LeanTween.cancel(pingPongTween.uniqueId);

        throwManager.NextState();
    }

    public override void OnReset()
    {
        angle = 0;
        
        if (pingPongTween != null)
            LeanTween.cancel(pingPongTween.uniqueId);
    }

    private void Blunder()
    {
        angle = 0;

        throwManager.Force /= 2;
        
        angleController.Blunder();
        
        OnClick();
    }
}