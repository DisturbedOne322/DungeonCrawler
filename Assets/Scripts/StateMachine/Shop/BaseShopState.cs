using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using StateMachine.Core;
using UI.BattleMenu;

namespace StateMachine.Shop
{
    public abstract class BaseShopState : BaseState<BaseShopState, ShopStateMachine>, IUiInputHandler
    {
        private readonly PlayerInputProvider _playerInputProvider;
        public readonly MenuItemsUpdater MenuItemsUpdater;

        protected List<MenuItemData> MenuItems = new();

        public BaseShopState(
            PlayerInputProvider playerInputProvider,
            MenuItemsUpdater menuItemsUpdater)
        {
            _playerInputProvider = playerInputProvider;
            MenuItemsUpdater = menuItemsUpdater;
        }

        public virtual void OnUISubmit()
        {
        }

        public virtual void OnUIBack()
        {
        }

        public virtual void OnUIUp()
        {
        }

        public virtual void OnUIDown()
        {
        }

        public virtual void OnUILeft()
        {
        }

        public virtual void OnUIRight()
        {
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
            MenuItemsUpdater.SetMenuItems(MenuItems, false);
        }

        public abstract void InitMenuItems();

        private void SubscribeToInputEvents()
        {
            _playerInputProvider.AddUiInputOwner(this);
        }

        private void UnsubscribeFromInputEvents()
        {
            _playerInputProvider.RemoveUiInputOwner(this);
        }
    }
}