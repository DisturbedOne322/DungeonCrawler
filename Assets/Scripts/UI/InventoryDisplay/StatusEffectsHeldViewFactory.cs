using System.Collections.Generic;
using UI.Menus;
using UI.Menus.Data;
using UI.Menus.MenuItemViews;

namespace UI.InventoryDisplay
{
    public class StatusEffectsHeldViewFactory
    {
        private readonly MenuItemViewFactory _menuItemViewFactory;

        public StatusEffectsHeldViewFactory(MenuItemViewFactory menuItemViewFactory)
        {
            _menuItemViewFactory = menuItemViewFactory;
        }

        public List<BaseMenuItemView> CreateViews(List<MenuItemData> itemsData)
        {
            List<BaseMenuItemView> result = new();
            foreach (var item in itemsData)
            {
                result.Add(_menuItemViewFactory.CreateStatusEffectMenuItem(item as StatusEffectMenuItemData));
            }

            return result;
        }
    }
}