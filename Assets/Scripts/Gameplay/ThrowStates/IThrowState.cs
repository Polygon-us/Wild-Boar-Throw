public interface IThrowState
{
    public ThrowController Controller { get; set; }
    
    public void OnEnterState(ThrowController controller);
    public void OnExitState();
    public void OnUpdate(float deltaTime);
    public void OnClick();
}
