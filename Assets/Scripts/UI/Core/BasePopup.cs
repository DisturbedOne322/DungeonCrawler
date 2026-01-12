using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace UI.Core
{
    public abstract class BasePopup : MonoBehaviour
    {
        [SerializeField] private PopupAnimator _popupAnimator;

        public readonly Subject<Unit> OnOpenCalled = new();
        public readonly Subject<Unit> OnOpened = new ();
        public readonly Subject<Unit> OnCloseCalled = new();
        public readonly Subject<Unit> OnDestroyed = new();
        
        private void Awake()
        {
            InitializePopup();
        }

        public async UniTask ShowPopup()
        {
            OnOpenCalled.OnNext(Unit.Default);
            await _popupAnimator.PlayShowAnim();
            OnOpened.OnNext(Unit.Default);
        }

        public async UniTask HidePopup()
        {
            OnCloseCalled.OnNext(Unit.Default);
            await _popupAnimator.PlayHideAnim(DestroyPopup);
            OnDestroyed.OnNext(Unit.Default);
        }

        private void DestroyPopup()
        {
            Destroy(gameObject);
        }

        protected virtual void InitializePopup()
        {
            
        }
    }
}