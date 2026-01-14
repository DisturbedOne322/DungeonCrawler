using System.Collections.Generic;
using Gameplay.Player;
using Gameplay.Units;
using UI.Menus;
using UI.Menus.Data;
using UniRx;

namespace UI.InventoryDisplay
{
    public abstract class BaseInventoryDisplayMenu : BaseUIInputHandler
    {
        protected readonly PlayerInputProvider InputProvider;
        protected readonly List<MenuItemData> Items = new();

        protected readonly MenuItemsUpdater ItemsUpdater = new();
        protected readonly PlayerUnit Player;

        public Subject<Unit> OnBack = new();

        protected BaseInventoryDisplayMenu(PlayerUnit player, PlayerInputProvider inputProvider)
        {
            Player = player;
            InputProvider = inputProvider;
        }

        public void TakeControls()
        {
            InputProvider.AddUiInputOwner(this);
        }

        public void RemoveControls()
        {
            InputProvider.RemoveUiInputOwner(this);
        }

        public override void OnUIUp()
        {
            ItemsUpdater.UpdateSelection(-1);
        }

        public override void OnUIDown()
        {
            ItemsUpdater.UpdateSelection(+1);
        }

        public override void OnUIBack()
        {
            OnBack?.OnNext(Unit.Default);
        }

        public Menu CreateMenu()
        {
            Items.Clear();
            BuildItems(Items);
            ItemsUpdater.SetMenuItems(Items);

            return new Menu(Items, ItemsUpdater);
        }

        protected abstract void BuildItems(List<MenuItemData> target);
    }
}