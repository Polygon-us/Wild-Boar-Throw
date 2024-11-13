public class AngleState : IThrowState
{
    public ThrowController Controller { get; set; }

    private int leanId;
    
    public void OnEnterState(ThrowController controller)
    {
        Controller = controller;
        
        // LeanTween.
        
        // Controller.AngleController.
    }
    
    public void OnExitState()
    {
        
    }

    public void OnUpdate(float deltaTime)
    {
    }

    public void OnClick()
    {
    }
}
