using UnityEngine;

public class ForceState : IThrowState
{
    private float force;
    private float chargeTimer;
    private int numClicks;

    private LTDescr timerTween; 
        
    public ThrowManager Manager { get; set; }

    public void OnEnterState(ThrowManager manager)
    {
        Manager = manager;
        
        Manager.ForceController.ForceSlider.maxValue = Manager.ForceController.MaxForce;
        Manager.ForceController.ForceSlider.minValue = 0f;
        Manager.ForceController.ForceSlider.value = 0f;
        
        Manager.ForceController.StateText.text = $"Charging\n{numClicks} clicks\n{force} N";
        
        timerTween = LeanTween.value(1,0, Manager.ForceController.ForceChargeTime)
            .setOnUpdate(t => Manager.ForceController.TimerSlider.value = t);
    }

    public void OnExitState()
    {
        LeanTween.cancel(timerTween.uniqueId);
    }
    
    public void OnUpdate()
    {
        chargeTimer += Time.deltaTime;

        UpdateForce(-Manager.ForceController.MaxForce * Manager.ForceController.DecrementPercentage * Time.deltaTime);

        if (chargeTimer >= Manager.ForceController.ForceChargeTime)
        {
            Release();
            chargeTimer = 0f;
        }
    }

    public void OnClick()
    {
        numClicks++;

        float forceResistance = Manager.ForceController.ChargeCurve.Evaluate(force / Manager.ForceController.MaxForce);

        UpdateForce(forceResistance * Manager.ForceController.MaxForce * Manager.ForceController.IncrementPercentage);

        Manager.ForceController.StateText.text = $"Charging\n{numClicks} clicks";
    }
    
    private void UpdateForce(float delta)
    {
        force = Mathf.Clamp(force + delta, 0f, Manager.ForceController.MaxForce);

        Manager.ForceController.ForceSlider.value = force;
    }
    
    private void Release()
    {
        Manager.ForceController.StateText.text = $"Charging\n{numClicks} clicks\n{force} N";

        Manager.Force = force;
        
        LeanTween.cancel(timerTween.uniqueId);

        Manager.ChangeState(new CountState());
    }
}
