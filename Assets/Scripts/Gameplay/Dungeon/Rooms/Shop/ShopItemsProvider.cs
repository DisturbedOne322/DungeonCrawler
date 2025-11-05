using System.Collections.Generic;
using Gameplay.Dungeon.Rooms.BaseSellableItems;

namespace Gameplay.Dungeon.Rooms.Shop
{
    public class ShopItemsProvider
    {
        private readonly ShopItemsConfig _shopItemsConfig;

        public ShopItemsProvider(ShopItemsConfig shopItemsConfig) => _shopItemsConfig = shopItemsConfig;

        public bool AnyConsumablesSold() => _shopItemsConfig.ConsumableItems.Count > 0;
        public IReadOnlyList<SoldItemModel> CreateConsumablesForSale() => 
            CreateItemsList(_shopItemsConfig.ConsumableItems);
        
        public bool AnyEquipmentSold() => _shopItemsConfig.EquipmentItems.Count > 0;
        public IReadOnlyList<SoldItemModel> CreateEquipmentForSale() => 
            CreateItemsList(_shopItemsConfig.EquipmentItems);
        
        public bool AnySkillsSold() => _shopItemsConfig.SkillItems.Count > 0;
        public IReadOnlyList<SoldItemModel> CreateSkillsForSale() => 
            CreateItemsList(_shopItemsConfig.SkillItems);
        
        public bool AnyStatusEffectsSold() => _shopItemsConfig.StatusEffectItems.Count > 0;
        public IReadOnlyList<SoldItemModel> CreateStatusEffectsForSale() => 
            CreateItemsList(_shopItemsConfig.StatusEffectItems);

        private IReadOnlyList<SoldItemModel> CreateItemsList(IReadOnlyList<ISellableItemData> dataList)
        {
            List<SoldItemModel> items = new();

            foreach (var data in dataList)
            {
                if (!TryCreateShopItemModel(data, out var model))
                    continue;

                items.Add(model);
            }

            return items;
        }

        private bool TryCreateShopItemModel(ISellableItemData data, out SoldItemModel model)
        {
            if (!data.Item)
            {
                model = null;
                return false;
            }

            model = new SoldItemModel(data);
            return true;
        }
    }
}