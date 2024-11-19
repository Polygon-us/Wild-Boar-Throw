public class AngleState : IThrowState
{
    public ThrowManager Manager { get; set; }

    private LTDescr pingPongTween;
    private float angle;

    public void OnEnterState(ThrowManager manager)
    {
        Manager = manager;

        pingPongTween = LeanTween.value(Manager.AngleController.MinAngle, Manager.AngleController.MaxAngle, Manager.AngleController.AnglePingPongTime)
            .setOnUpdate(t =>
            {
                angle = t;
                manager.AngleController.AngleSlider.value = angle;
            })
            .setLoopPingPong(Manager.AngleController.PingPongCount)
            .setOnComplete(Blunder);
    }

    public void OnExitState()
    {
        LeanTween.cancel(pingPongTween.uniqueId);
    }

    public void OnUpdate()
    {
    }
    
    private void Blunder()
    {
        angle = 0;

        Manager.Force /= 2;
        
        Manager.AngleController.Blunder();
        
        OnClick();
    }

    public void OnClick()
    {
        Manager.Angle = angle;

        LeanTween.cancel(pingPongTween.uniqueId);

        Manager.ChangeState(new ReleaseState());
    }
}