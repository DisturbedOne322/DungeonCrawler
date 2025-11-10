using System.Collections.Generic;
using Gameplay.Dungeon.Rooms.BaseSellableItems;

namespace Gameplay.Dungeon.Rooms.Shop
{
    public class ShopItemsProvider
    {
        private readonly ShopItemsConfig _shopItemsConfig;

        public ShopItemsProvider(ShopItemsConfig shopItemsConfig)
        {
            _shopItemsConfig = shopItemsConfig;
        }

        public bool AnyConsumablesSold()
        {
            return _shopItemsConfig.ConsumableItems.Count > 0;
        }

        public IReadOnlyList<SoldItemModel> CreateConsumablesForSale()
        {
            return CreateItemsList(_shopItemsConfig.ConsumableItems);
        }

        public bool AnyEquipmentSold()
        {
            return _shopItemsConfig.EquipmentItems.Count > 0;
        }

        public IReadOnlyList<SoldItemModel> CreateEquipmentForSale()
        {
            return CreateItemsList(_shopItemsConfig.EquipmentItems);
        }

        public bool AnySkillsSold()
        {
            return _shopItemsConfig.SkillItems.Count > 0;
        }

        public IReadOnlyList<SoldItemModel> CreateSkillsForSale()
        {
            return CreateItemsList(_shopItemsConfig.SkillItems);
        }

        public bool AnyStatusEffectsSold()
        {
            return _shopItemsConfig.StatusEffectItems.Count > 0;
        }

        public IReadOnlyList<SoldItemModel> CreateStatusEffectsForSale()
        {
            return CreateItemsList(_shopItemsConfig.StatusEffectItems);
        }

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