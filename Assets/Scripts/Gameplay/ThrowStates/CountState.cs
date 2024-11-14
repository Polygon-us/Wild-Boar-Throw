using Cysharp.Threading.Tasks;

public class CountState : IThrowState
{
    public ThrowManager Manager { get; set; }

    public void OnEnterState(ThrowManager manager)
    {
        Manager = manager;

        Manager.CountController.Open();

        CountDown();
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

    private async void CountDown()
    {
        int count = Manager.CountController.Count;

        while (count > 0)
        {
            Manager.CountController.CountText.text = count.ToString();

            await UniTask.Delay(1000);

            count--;
        }

        Manager.CountController.Close();

        Manager.ChangeState(new AngleState());
    }
}