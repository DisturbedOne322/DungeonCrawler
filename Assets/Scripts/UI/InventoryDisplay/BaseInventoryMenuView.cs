using System;
using UI.Menus;
using UniRx;
using UnityEngine;

namespace UI.InventoryDisplay
{
    public abstract class BaseInventoryMenuView : BaseDisplayMenuView
    {
        [SerializeField] protected MenuPageView MenuPageView;
        
        protected BaseInventoryDisplayMenu DisplayMenu;

        private IDisposable _disposable;
        
        protected override void Initialize()
        {
            _disposable = DisplayMenu.OnBack.Subscribe(_ => InvokeOnBack());
        }

        private void OnDestroy() => _disposable?.Dispose();

        public override void Select() => DisplayMenu.TakeControls();

        public override void Deselect() => DisplayMenu.RemoveControls();
    }
}