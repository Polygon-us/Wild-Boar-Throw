public interface IThrowState
{
    public ThrowManager Manager { get; set; }
    
    public void OnEnterState(ThrowManager manager);
    public void OnExitState();
    public void OnUpdate();
    public void OnClick();
}
