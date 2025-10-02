using Cysharp.Threading.Tasks;
using UnityEngine;

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