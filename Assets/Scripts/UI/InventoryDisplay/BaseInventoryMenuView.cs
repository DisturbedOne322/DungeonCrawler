using System;
using Animations;
using UI.Menus;
using UniRx;
using UnityEngine;

namespace UI.InventoryDisplay
{
    public abstract class BaseInventoryMenuView : BaseDisplayMenuView
    {
        [SerializeField] protected MenuPageView MenuPageView;
        [SerializeField] private MenuPageSelectAnimator _animator;
        
        protected BaseInventoryDisplayMenu DisplayMenu;

        private IDisposable _disposable;
        
        protected override void Initialize()
        {
            _disposable = DisplayMenu.OnBack.Subscribe(_ => InvokeOnBack());
        }

        private void OnDestroy() => _disposable?.Dispose();

        public override void Select()
        {
            DisplayMenu.TakeControls();
            _animator.Select(Image);
        }

        public override void Deselect()
        {
            DisplayMenu.RemoveControls();
            _animator.Deselect(Image);
        }
    }
}