using UnityEngine;

public class ForceState : IThrowState
{
    private float force;
    private float chargeTimer;
    private int numClicks;
    
    public ThrowController Controller { get; set; }

    public void OnEnterState(ThrowController controller)
    {
        Controller = controller;
        
        Controller.ForceSlider.maxValue = Controller.MaxForce;
        Controller.ForceSlider.minValue = 0f;
        Controller.ForceSlider.value = 0f;
    }

    public void OnExitState()
    {
        
    }
    
    public void OnUpdate(float deltaTime)
    {
        chargeTimer += Time.deltaTime;

        UpdateForce(-Controller.MaxForce * Controller.DecrementPercentage * Time.deltaTime);

        if (chargeTimer >= Controller.ChargeTime)
        {
            Release();
            chargeTimer = 0f;
        }
    }

    public void OnClick()
    {
        numClicks++;

        float forceResistance = Controller.ChargeCurve.Evaluate(force / Controller.MaxForce);

        UpdateForce(forceResistance * Controller.MaxForce * Controller.IncrementPercentage);

        Controller.StateText.text = $"Charging\n{numClicks} clicks";
    }
    
    private void UpdateForce(float delta)
    {
        force = Mathf.Clamp(force + delta, 0f, Controller.MaxForce);

        Controller.ForceSlider.value = force;
    }
    
    private void Release()
    {
        Controller.StateText.text = $"Charging\n{numClicks} clicks\n{force} N";

        Controller.Force = force;
        
        Controller.ChangeState(new AngleState());
    }
}
