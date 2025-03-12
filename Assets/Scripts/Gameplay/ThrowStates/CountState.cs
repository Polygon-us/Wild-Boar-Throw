using Cysharp.Threading.Tasks;
using UnityEngine;

public class CountState : StateBase
{
    [SerializeField] private CountController countController;
    [SerializeField] private CamerasController camerasController;
    
    public override void OnEnterState(StateMachine stateMachine)
    {
        base.OnEnterState(stateMachine);
        
        countController.Open();
        
        camerasController.FollowCamera();

        CountDown().Forget();
    }
    
    private async UniTaskVoid CountDown()
    {
        int count = countController.Count;

        while (count > 0)
        {
            countController.CountText.text = count.ToString();

            await UniTask.Delay(1000);

            count--;
        }

        countController.Close();

        StateMachine.NextState();
    }
}