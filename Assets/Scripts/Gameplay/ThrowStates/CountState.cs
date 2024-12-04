using Cysharp.Threading.Tasks;
using UnityEngine;

public class CountState : StateBase
{
    [SerializeField] private CountController countController;
    
    public override void OnEnterState(StateMachine stateMachine)
    {
        base.OnEnterState(stateMachine);
        
        countController.Open();

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