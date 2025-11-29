using System;
using UI.BattleMenu;
using UI.Menus;

namespace Gameplay.Dungeon.Rooms.BasePurchasableItems
{
    public class PurchasableItemMenuItemData : MenuItemData
    {
        public PurchasableItemMenuItemData(PurchasedItemModel purchasedItemModel,
            Func<bool> selectableFunc,
            Action onSelected) :
            base(purchasedItemModel.ItemData.Item.Name,
                selectableFunc,
                onSelected,
                MenuItemType.ShopItem,
                purchasedItemModel.ItemData.Item.Description,
                purchasedItemModel.ItemData.Amount)
        {
            PurchasedItemModel = purchasedItemModel;
        }

        public PurchasedItemModel PurchasedItemModel { get; }
    }
}