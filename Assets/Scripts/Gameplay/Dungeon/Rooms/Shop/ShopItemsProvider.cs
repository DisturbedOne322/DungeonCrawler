using System.Collections.Generic;
using Gameplay.Dungeon.Rooms.BasePurchasableItems;

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

        public IReadOnlyList<PurchasedItemModel> CreateConsumablesForSale()
        {
            return CreateItemsList(_shopItemsConfig.ConsumableItems);
        }

        public bool AnyEquipmentSold()
        {
            return _shopItemsConfig.EquipmentItems.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> CreateEquipmentForSale()
        {
            return CreateItemsList(_shopItemsConfig.EquipmentItems);
        }

        public bool AnySkillsSold()
        {
            return _shopItemsConfig.SkillItems.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> CreateSkillsForSale()
        {
            return CreateItemsList(_shopItemsConfig.SkillItems);
        }

        public bool AnyStatusEffectsSold()
        {
            return _shopItemsConfig.StatusEffectItems.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> CreateStatusEffectsForSale()
        {
            return CreateItemsList(_shopItemsConfig.StatusEffectItems);
        }

        private IReadOnlyList<PurchasedItemModel> CreateItemsList(IReadOnlyList<IPurchasableItemData> dataList)
        {
            List<PurchasedItemModel> items = new();

            foreach (var data in dataList)
            {
                if (!TryCreateShopItemModel(data, out var model))
                    continue;

                items.Add(model);
            }

            return items;
        }

        private bool TryCreateShopItemModel(IPurchasableItemData data, out PurchasedItemModel model)
        {
            if (!data.Item)
            {
                model = null;
                return false;
            }

            model = new PurchasedItemModel(data);
            return true;
        }
    }
}