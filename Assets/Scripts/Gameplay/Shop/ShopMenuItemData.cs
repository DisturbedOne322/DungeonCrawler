using System;
using UI.BattleMenu;
namespace Gameplay.Shop
{
    public class ShopMenuItemData : MenuItemData
    {
        private readonly ShopItemModel _shopItemModel;
        
        public ShopItemModel ShopItemModel => _shopItemModel;
        
        public ShopMenuItemData(ShopItemModel shopItemModel,
            Func<bool> selectableFunc,
            Action onSelected) : 
            base(shopItemModel.ItemData.Item.Name, 
                selectableFunc,
                onSelected, 
                MenuItemType.ShopItem, 
                shopItemModel.ItemData.Item.Description, 
                shopItemModel.ItemData.Amount)
        {
            _shopItemModel = shopItemModel;
        }
    }
}