using System.Collections.Generic;
using UI.Menus;
using UI.Menus.Data;

namespace UI.InventoryDisplay
{
    public class Menu
    {
        public readonly List<MenuItemData> Items;
        public readonly MenuItemsUpdater ItemsUpdater;

        public Menu(List<MenuItemData> items, MenuItemsUpdater itemsUpdater)
        {
            Items = items;
            ItemsUpdater = itemsUpdater;
        }
    }
}