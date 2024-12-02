public class ReleaseState : IThrowState
{
    public ThrowManager Manager { get; set; }
    
    public void OnEnterState(ThrowManager manager)
    {
        manager.Release();
    }

    public void OnExitState()
    {
    }

    public void OnUpdate()
    {
    }

    public void OnClick()
    {
    }
}