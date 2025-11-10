using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Extensions;
using UniRx;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerInputProvider : IDisposable
    {
        private readonly PlayerInputActions _inputActions = new();
        private readonly Stack<IUiInputHandler> _uiInputHandlers = new();

        public readonly Subject<Unit> OnGoForward = new();
        public readonly Subject<Unit> OnGoLeft = new();
        public readonly Subject<Unit> OnGoRight = new();

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
            _inputActions.UI.Up.performed += _ => GetActiveUiOwner()?.OnUIUp();
            _inputActions.UI.Down.performed += _ => GetActiveUiOwner()?.OnUIDown();
            _inputActions.UI.Submit.performed += _ => GetActiveUiOwner()?.OnUISubmit();
            _inputActions.UI.Back.performed += _ => GetActiveUiOwner()?.OnUIBack();
            _inputActions.UI.Left.performed += _ => GetActiveUiOwner()?.OnUILeft();
            _inputActions.UI.Right.performed += _ => GetActiveUiOwner()?.OnUIRight();
        }

        public void EnableMovementInput(bool enable)
        {
            if (enable)
                _inputActions.Decision.Enable();
            else
                _inputActions.Decision.Disable();
        }

        public async UniTask EnableUIInputUntil(UniTask task, IUiInputHandler handler)
        {
            AddUiInputOwner(handler);
            await task;
            RemoveUiInputOwner(handler);
        }

        public async UniTask<T> EnableUIInputUntil<T>(UniTask<T> task, IUiInputHandler handler)
        {
            AddUiInputOwner(handler);
            var result = await task;
            RemoveUiInputOwner(handler);
            return result;
        }

        public void AddUiInputOwner(IUiInputHandler inputHandler)
        {
            _uiInputHandlers.Push(inputHandler);
            if (_uiInputHandlers.Count == 1)
                _inputActions.UI.Enable();
        }

        public void RemoveUiInputOwner(IUiInputHandler inputHandler)
        {
            if (_uiInputHandlers.Count == 0)
            {
                Debug.LogWarning("Tried to remove UI input handler when stack is empty");
                return;
            }

            if (_uiInputHandlers.Peek() == inputHandler)
                _uiInputHandlers.Pop();
            else
                Debug.LogWarning("Tried to pop non-top UI handler (possible mismatched calls)");

            if (_uiInputHandlers.Count == 0)
                _inputActions.UI.Disable();
        }

        private IUiInputHandler GetActiveUiOwner()
        {
            return _uiInputHandlers.PeekOrNull();
        }
    }
}