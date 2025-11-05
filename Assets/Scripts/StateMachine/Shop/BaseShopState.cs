using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using StateMachine.Core;
using UI.BattleMenu;
using UniRx;

namespace StateMachine.Shop
{
    public abstract class BaseShopState : BaseState<BaseShopState, ShopStateMachine>
    {
        public readonly MenuItemsUpdater MenuItemsUpdater;
        protected readonly PlayerInputProvider PlayerInputProvider;

        protected CompositeDisposable Disposables;

        protected List<MenuItemData> MenuItems = new();

        public BaseShopState(
            PlayerInputProvider playerInputProvider,
            MenuItemsUpdater menuItemsUpdater)
        {
            PlayerInputProvider = playerInputProvider;
            MenuItemsUpdater = menuItemsUpdater;
        }
        
        public override UniTask EnterState()
        {
            SubscribeToInputEvents();
            return UniTask.CompletedTask;
        }
        
        public override UniTask ExitState()
        {
            UnsubscribeFromInputEvents();
            return UniTask.CompletedTask;
        }

        public void LoadMenuItems()
        {
            MenuItems.Clear();
            InitMenuItems();
            MenuItemsUpdater.SetMenuItems(MenuItems);
            MenuItemsUpdater.ResetSelection(false);
        }
        
        public abstract void InitMenuItems();

        protected abstract void SubscribeToInputEvents();

        private void UnsubscribeFromInputEvents() => Disposables?.Dispose();
    }
}