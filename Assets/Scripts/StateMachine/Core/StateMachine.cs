using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;

namespace States
{
    public class StateMachine<TState, TMachine>
        where TState : IState<TMachine>
        where TMachine : StateMachine<TState, TMachine>
    {
        private readonly Dictionary<Type, TState> _states = new();
        private TState _previousState;

        public ReactiveProperty<TState> CurrentState = new ();
        public TState PreviousState => _previousState;

        public StateMachine(IEnumerable<TState> states)
        {
            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
                state.SetStateMachine((TMachine)this);
            }
        }

        public async UniTask GoToState<T>() where T : TState
            => await ChangeState(typeof(T));

        public async UniTask GoToPrevState()
        {
            if (_previousState == null)
                throw new Exception("Previous state is null");

            await ChangeState(_previousState.GetType());
        }

        public void Reset()
        {
            CurrentState.Value.ExitState();
            CurrentState.Value = default;
            _previousState = default;
        }

        private async UniTask ChangeState(Type type)
        {
            if (CurrentState.Value != null)
            {
                _previousState = CurrentState.Value;
                await CurrentState.Value.ExitState();
            }

            CurrentState.Value = _states[type];
            await CurrentState.Value.EnterState();
        }
    }
}