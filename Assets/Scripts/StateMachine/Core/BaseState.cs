using Cysharp.Threading.Tasks;

namespace StateMachine.Core
{
    public abstract class BaseState<TState, TMachine> : IState<TMachine>
        where TState : BaseState<TState, TMachine>
        where TMachine : StateMachine<TState, TMachine>
    {
        protected TMachine StateMachine { get; private set; }

        void IState<TMachine>.SetStateMachine(TMachine machine) => StateMachine = machine;

        public abstract UniTask EnterState();
        public abstract UniTask ExitState();

        protected UniTask GoToState<T>() where T : TState => StateMachine.GoToState<T>();
    }
}