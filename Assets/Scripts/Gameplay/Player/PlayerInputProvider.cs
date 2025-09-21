using System;
using UniRx;

namespace Gameplay.Player
{
    public class PlayerInputProvider : IDisposable
    {
        private readonly PlayerInputActions _inputActions = new ();

        private readonly Subject<Unit> _onGoLeft = new();
        private readonly Subject<Unit> _onGoForward = new();
        private readonly Subject<Unit> _onGoRight = new();
        
        private readonly Subject<Unit> _onUiUp = new();
        private readonly Subject<Unit> _onUiDown = new();
        private readonly Subject<Unit> _onUiSubmit = new();
        private readonly Subject<Unit> _onUiBack = new();
        
        public IObservable<Unit> OnGoLeft => _onGoLeft;
        public IObservable<Unit> OnGoForward => _onGoForward;
        public IObservable<Unit> OnGoRight => _onGoRight;
        
        public IObservable<Unit> OnUiUp => _onUiUp;
        public IObservable<Unit> OnUiDown => _onUiDown;
        public IObservable<Unit> OnUiSubmit => _onUiSubmit;
        public IObservable<Unit> OnUiBack => _onUiBack;
        
        public PlayerInputProvider()
        {
            _inputActions.Decision.GoLeft.performed += ctx => _onGoLeft.OnNext(Unit.Default);
            _inputActions.Decision.GoForward.performed += ctx => _onGoForward.OnNext(Unit.Default);
            _inputActions.Decision.GoRight.performed += ctx => _onGoRight.OnNext(Unit.Default);
            
            _inputActions.UI.Up.performed += ctx => _onUiUp.OnNext(Unit.Default);
            _inputActions.UI.Down.performed += ctx => _onUiDown.OnNext(Unit.Default);
            _inputActions.UI.Submit.performed += ctx => _onUiSubmit.OnNext(Unit.Default);
            _inputActions.UI.UiBack.performed += ctx => _onUiBack.OnNext(Unit.Default);
        }

        public void EnableMovementInput(bool enable)
        {
            if(enable)
                _inputActions.Decision.Enable();
            else
                _inputActions.Decision.Disable();
        }
        
        public void EnableUiInput(bool enable)
        {
            if(enable)
                _inputActions.UI.Enable();
            else
                _inputActions.UI.Disable();
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