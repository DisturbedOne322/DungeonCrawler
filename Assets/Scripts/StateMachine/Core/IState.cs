using Cysharp.Threading.Tasks;

namespace StateMachine.Core
{
    public interface IState<TMachine>
    {
        void SetStateMachine(TMachine machine);
        UniTask EnterState();
        UniTask ExitState();
    }
}