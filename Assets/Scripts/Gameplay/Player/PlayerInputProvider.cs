using System;
using UniRx;

namespace Gameplay.Player
{
    public class PlayerInputProvider : IDisposable
    {
        private readonly PlayerInputActions _inputActions = new ();

        public IObservable<Unit> OnGoLeft => _onGoLeft;
        public IObservable<Unit> OnGoForward => _onGoForward;
        public IObservable<Unit> OnGoRight => _onGoRight;

        private readonly Subject<Unit> _onGoLeft = new();
        private readonly Subject<Unit> _onGoForward = new();
        private readonly Subject<Unit> _onGoRight = new();

        public PlayerInputProvider()
        {
            _inputActions.Decision.GoLeft.performed += ctx => _onGoLeft.OnNext(Unit.Default);
            _inputActions.Decision.GoForward.performed += ctx => _onGoForward.OnNext(Unit.Default);
            _inputActions.Decision.GoRight.performed += ctx => _onGoRight.OnNext(Unit.Default);
        }

        public void EnableInput(bool enable)
        {
            if(enable)
                _inputActions.Enable();
            else
                _inputActions.Disable();
        }
        
        public void Dispose()
        {
            _inputActions.Disable();
            _onGoLeft.Dispose();
            _onGoForward.Dispose();
            _onGoRight.Dispose();
        }
    }
}