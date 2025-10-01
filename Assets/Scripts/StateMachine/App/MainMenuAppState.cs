using Cysharp.Threading.Tasks;
using UnityEngine;

namespace StateMachine.App
{
    public class MainMenuAppState : BaseAppState
    {
        public override async UniTask EnterState()
        {
            Debug.Log("Entered: Main menu");
            await GoToState<GameplayAppState>();
        }

        public override UniTask ExitState()
        {
            Debug.Log("Left: Main menu");
            return UniTask.CompletedTask;
        }
    }
}