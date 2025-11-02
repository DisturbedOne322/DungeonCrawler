using System.Collections.Generic;

namespace Gameplay.Shop
{
    public class ShopItemsProvider
    {
        private readonly ShopItemsConfig _shopItemsConfig;

        public ShopItemsProvider(ShopItemsConfig shopItemsConfig) => _shopItemsConfig = shopItemsConfig;

        public bool AnyConsumablesSold() => _shopItemsConfig.ConsumableItems.Count > 0;
        public IReadOnlyList<ShopItemModel> CreateConsumablesForSale() => 
            CreateItemsList(_shopItemsConfig.ConsumableItems);
        
        public bool AnyEquipmentSold() => _shopItemsConfig.EquipmentItems.Count > 0;
        public IReadOnlyList<ShopItemModel> CreateEquipmentForSale() => 
            CreateItemsList(_shopItemsConfig.EquipmentItems);
        
        public bool AnySkillsSold() => _shopItemsConfig.SkillItems.Count > 0;
        public IReadOnlyList<ShopItemModel> CreateSkillsForSale() => 
            CreateItemsList(_shopItemsConfig.SkillItems);
        
        public bool AnyStatusEffectsSold() => _shopItemsConfig.StatusEffectItems.Count > 0;
        public IReadOnlyList<ShopItemModel> CreateStatusEffectsForSale() => 
            CreateItemsList(_shopItemsConfig.StatusEffectItems);

        private IReadOnlyList<ShopItemModel> CreateItemsList<T>(IReadOnlyList<ShopItemData<T>> dataList)
            where T : BaseGameItem
        {
            List<ShopItemModel> items = new();

            foreach (var data in dataList)
            {
                if (!TryCreateShopItemModel(data, out var model))
                    continue;

                items.Add(model);
            }

            return items;
        }

        private bool TryCreateShopItemModel<T>(ShopItemData<T> data, out ShopItemModel model)
            where T : BaseGameItem
        {
            if (!data.Item)
            {
                model = null;
                return false;
            }

            model = new ShopItemModel(data);
            return true;
        }
    }
}