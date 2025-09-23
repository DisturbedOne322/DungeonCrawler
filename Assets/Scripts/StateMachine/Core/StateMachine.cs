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
        protected readonly Dictionary<Type, TState> States = new();
        
        private TState _currentState;
        private TState _previousState;

        public readonly Subject<TState> OnStateChanged = new();
        
        public TState CurrentState => _currentState;
        public TState PreviousState => _previousState;

        public StateMachine(IEnumerable<TState> states)
        {
            foreach (var state in states)
            {
                States.Add(state.GetType(), state);
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
            _currentState.ExitState();
            _currentState = default;
            _previousState = default;
        }

        private async UniTask ChangeState(Type type)
        {
            if (CurrentState != null)
            {
                _previousState = _currentState;
                await _currentState.ExitState();
            }

            _currentState = States[type];
            await _currentState.EnterState();
            
            OnStateChanged.OnNext(_currentState);
        }
    }
}