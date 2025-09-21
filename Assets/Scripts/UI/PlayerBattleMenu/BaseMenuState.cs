using System.Collections.Generic;
using Gameplay.Player;

namespace UI.PlayerBattleMenu
{
    public abstract class BaseMenuState
    {
        protected MenuItemsUpdater MenuItemsUpdater;
        protected PlayerInputProvider PlayerInputProvider;
        protected BattleMenuController BattleMenuController;
        
        protected int SelectedItemIndex;
        
        protected List<MenuItemView> MenuItems;

        public BaseMenuState(MenuItemsUpdater menuItemsUpdater, PlayerInputProvider playerInputProvider, BattleMenuController battleMenuController)
        {
            MenuItemsUpdater = menuItemsUpdater;
            PlayerInputProvider = playerInputProvider;
            BattleMenuController = battleMenuController;
        }

        public void Initialize(List<MenuItemView> menuItems)
        {
            MenuItems = menuItems;
        }
        public abstract void EnterState();
        public abstract void ExitState();
    }
}