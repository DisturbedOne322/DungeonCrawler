using System.Collections.Generic;
using Gameplay.Consumables;
using Gameplay.Player;
using Gameplay.Units;
using UI.Menus.Data;

namespace UI.InventoryDisplay.Consumables
{
    public class ConsumablesHeldMenu : BaseInventoryDisplayMenu
    {
        public ConsumablesHeldMenu(PlayerUnit player, 
            PlayerInputProvider inputProvider) : base(player, inputProvider)
        {
        }

        protected override void BuildItems(List<MenuItemData> target)
        {
            var items = Player.UnitInventoryData.Consumables;
            
            Dictionary<BaseConsumable, int> itemsDict = new();

            foreach (var item in items)
                if (!itemsDict.TryAdd(item, 1))
                    itemsDict[item]++;

            foreach (var item in itemsDict) 
                target.Add(MenuItemData.ForConsumableItem(item.Key, () => true, () => {}, item.Value));
        }
    }
}