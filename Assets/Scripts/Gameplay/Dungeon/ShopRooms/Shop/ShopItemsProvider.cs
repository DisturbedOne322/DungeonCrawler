using System.Collections.Generic;
using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;

namespace Gameplay.Dungeon.ShopRooms.Shop
{
    public class ShopItemsProvider
    {
        private readonly ShopRoom _shopRoom;

        public ShopItemsProvider(ShopRoom shopRoom)
        {
            _shopRoom = shopRoom;
        }

        public bool AnyConsumablesSold()
        {
            return _shopRoom.Config.ConsumableItems.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> CreateConsumablesForSale()
        {
            return CreateItemsList(_shopRoom.Config.ConsumableItems);
        }

        public bool AnyEquipmentSold()
        {
            return _shopRoom.Config.EquipmentItems.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> CreateEquipmentForSale()
        {
            return CreateItemsList(_shopRoom.Config.EquipmentItems);
        }

        public bool AnySkillsSold()
        {
            return _shopRoom.Config.SkillItems.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> CreateSkillsForSale()
        {
            return CreateItemsList(_shopRoom.Config.SkillItems);
        }

        public bool AnyStatusEffectsSold()
        {
            return _shopRoom.Config.StatusEffectItems.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> CreateStatusEffectsForSale()
        {
            return CreateItemsList(_shopRoom.Config.StatusEffectItems);
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