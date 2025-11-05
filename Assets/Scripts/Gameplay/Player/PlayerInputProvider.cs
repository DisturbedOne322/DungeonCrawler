using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerInputProvider : IDisposable
    {
        private readonly PlayerInputActions _inputActions = new();
        public readonly Subject<Unit> OnGoForward = new();
        public readonly Subject<Unit> OnGoLeft = new();
        public readonly Subject<Unit> OnGoRight = new();
        
        public readonly Subject<Unit> OnUiBack = new();
        public readonly Subject<Unit> OnUiDown = new();
        public readonly Subject<Unit> OnUiLeft = new();
        public readonly Subject<Unit> OnUiRight = new();
        public readonly Subject<Unit> OnUiSubmit = new();

        public readonly Subject<Unit> OnUiUp = new();

        private int _uiOwnersCount = 0;

        public PlayerInputProvider()
        {
            SubscribeMovementActions();
            SubscribeUiActions();
        }

        public void Dispose()
        {
            _inputActions.Disable();
        }

        private void SubscribeMovementActions()
        {
            _inputActions.Decision.GoLeft.performed += ctx => OnGoLeft.OnNext(Unit.Default);
            _inputActions.Decision.GoForward.performed += ctx => OnGoForward.OnNext(Unit.Default);
            _inputActions.Decision.GoRight.performed += ctx => OnGoRight.OnNext(Unit.Default);
        }

        private void SubscribeUiActions()
        {
            _inputActions.UI.Up.performed += ctx => OnUiUp.OnNext(Unit.Default);
            _inputActions.UI.Down.performed += ctx => OnUiDown.OnNext(Unit.Default);
            _inputActions.UI.Submit.performed += ctx => OnUiSubmit.OnNext(Unit.Default);
            _inputActions.UI.Back.performed += ctx => OnUiBack.OnNext(Unit.Default);
            _inputActions.UI.Left.performed += ctx => OnUiLeft.OnNext(Unit.Default);
            _inputActions.UI.Right.performed += ctx => OnUiRight.OnNext(Unit.Default);
        }

        public void EnableMovementInput(bool enable)
        {
            if (enable)
                _inputActions.Decision.Enable();
            else
                _inputActions.Decision.Disable();
        }

        public async UniTask EnableUIInputUntil(UniTask task)
        {
            AddUiInputOwner();
            await task;
            RemoveUiInputOwner();
        }
        
        public async UniTask<T> EnableUIInputUntil<T>(UniTask<T> task)
        {
            AddUiInputOwner();
            var result = await task;
            RemoveUiInputOwner();
            return result;
        }

        private void AddUiInputOwner()
        {
            _uiOwnersCount++;  
            if (_uiOwnersCount == 1)
                _inputActions.UI.Enable();
        }
        
        private void RemoveUiInputOwner()
        {
            _uiOwnersCount--;
            if(_uiOwnersCount == 0)
                _inputActions.UI.Disable();

            if (_uiOwnersCount < 0)
            {
                Debug.LogError("TRIED TO DISABLE UI INPUT WITH NO OWNERS!");
                _uiOwnersCount = 0;
            }
        }
    }
}