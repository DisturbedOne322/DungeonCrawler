using System;
using UI.BattleMenu;
using UI.Menus;

namespace Gameplay.Dungeon.Rooms.BaseSellableItems
{
    public class SoldItemMenuItemData : MenuItemData
    {
        public SoldItemMenuItemData(SoldItemModel soldItemModel,
            Func<bool> selectableFunc,
            Action onSelected) :
            base(soldItemModel.ItemData.Item.Name,
                selectableFunc,
                onSelected,
                MenuItemType.ShopItem,
                soldItemModel.ItemData.Item.Description,
                soldItemModel.ItemData.Amount)
        {
            SoldItemModel = soldItemModel;
        }

        public SoldItemModel SoldItemModel { get; }
    }
}