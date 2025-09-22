using Cysharp.Threading.Tasks;

namespace States
{
    public interface IState<TMachine>
    {
        void SetStateMachine(TMachine machine);
        UniTask EnterState();
        UniTask ExitState();
    }
}