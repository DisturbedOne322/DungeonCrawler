using System;
using UniRx;

namespace Gameplay.Player
{
    public class PlayerInputProvider : IDisposable
    {
        private readonly PlayerInputActions _inputActions = new ();

        public readonly Subject<Unit> OnGoLeft = new();
        public readonly Subject<Unit> OnGoForward = new();
        public readonly Subject<Unit> OnGoRight = new();
        
        public readonly Subject<Unit> OnUiUp = new();
        public readonly Subject<Unit> OnUiDown = new();
        public readonly Subject<Unit> OnUiSubmit = new();
        public readonly Subject<Unit> OnUiBack = new();
        
        public PlayerInputProvider()
        {
            _inputActions.Decision.GoLeft.performed += ctx => OnGoLeft.OnNext(Unit.Default);
            _inputActions.Decision.GoForward.performed += ctx => OnGoForward.OnNext(Unit.Default);
            _inputActions.Decision.GoRight.performed += ctx => OnGoRight.OnNext(Unit.Default);
            
            _inputActions.UI.Up.performed += ctx => OnUiUp.OnNext(Unit.Default);
            _inputActions.UI.Down.performed += ctx => OnUiDown.OnNext(Unit.Default);
            _inputActions.UI.Submit.performed += ctx => OnUiSubmit.OnNext(Unit.Default);
            _inputActions.UI.UiBack.performed += ctx => OnUiBack.OnNext(Unit.Default);
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
        }
    }
}