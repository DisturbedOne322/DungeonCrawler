using Cysharp.Threading.Tasks;

namespace StateMachine.App
{
    public class MainMenuAppState : BaseAppState
    {
        public override async UniTask EnterState()
        {
            await GoToState<GameplayAppState>();
        }

        public override UniTask ExitState()
        {
            return UniTask.CompletedTask;
        }
    }
}