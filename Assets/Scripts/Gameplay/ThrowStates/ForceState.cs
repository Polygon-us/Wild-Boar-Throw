using ForceVisualizerAnimation;
using UnityEngine;

public class ForceState : StateBase
{
    [SerializeField] private ForceController forceController;
    [SerializeField] private ThrowManager throwManager;
    [SerializeField] private ForceVisualizerController forceVisualizerController;
    [SerializeField] private BoarThrower boarThrower;
    [SerializeField] private CamerasController camerasController;
    [SerializeField] private CrowdController crowdController;

    private float force;
    private float chargeTimer;
    private int numClicks;

    private LTDescr timerTween;

    private bool firstClick = false;


    public override void OnEnterState(StateMachine stateMachine)
    {
        base.OnEnterState(stateMachine);
        
        forceController.ForceSlider.maxValue = forceController.MaxForce;
        forceController.ForceSlider.minValue = 0f;
        forceController.ForceSlider.value = 0f;

        forceController.TimerSlider.value = forceController.ForceChargeTime;

        forceController.StateText.text = $"Charging\n{numClicks} clicks\n{force} N";
        
        forceVisualizerController.MovePlayableDirector(0);
    }

    public override void OnExitState()
    {
        if (timerTween != null)
            LeanTween.cancel(timerTween.uniqueId);
    }

    public override void OnUpdate()
    {
        if (!firstClick)
            return;

        chargeTimer += Time.deltaTime;

        UpdateForce(-forceController.MaxForce * forceController.DecrementPercentage * Time.deltaTime);
        
        boarThrower.MoveBoarWithStartingPosition();

        if (chargeTimer >= forceController.ForceChargeTime)
        {
            Release();
            chargeTimer = 0f;
        }
    }

    public override void OnClick()
    {
        if (!firstClick)
        {
            timerTween = LeanTween.value(1, 0, forceController.ForceChargeTime)
                .setOnUpdate(t => forceController.TimerSlider.value = t);

            firstClick = true;
        }

        numClicks++;

        float forceResistance = forceController.ChargeCurve.Evaluate(force / forceController.MaxForce);

        UpdateForce(forceResistance * forceController.MaxForce * forceController.IncrementPercentage);

        forceController.StateText.text = $"Charging\n{numClicks} clicks";
    }

    private void UpdateForce(float delta)
    {
        force = Mathf.Clamp(force + delta, 0f, forceController.MaxForce);
        
        forceController.ForceSlider.value = force;
        
        forceVisualizerController.MovePlayableDirector(force / forceController.MaxForce);
    }

    private void Release()
    {
        forceController.StateText.text = $"Charging\n{numClicks} clicks\n{force} N";

        LeanTween.cancel(timerTween.uniqueId);

        throwManager.Force = force;
        
        crowdController.MakeImpression(force / forceController.MaxForce);

        StateMachine.NextState();
    }

    public override void OnReset()
    {
        force = 0;
        chargeTimer = 0;
        numClicks = 0;
        firstClick = false;
        forceVisualizerController.MovePlayableDirector(0);
        crowdController.MakeImpression(0);
        camerasController.Reset();
        boarThrower.Reset();
    }
}