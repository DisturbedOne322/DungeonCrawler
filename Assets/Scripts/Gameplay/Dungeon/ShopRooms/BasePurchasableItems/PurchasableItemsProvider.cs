using System.Collections.Generic;
using Gameplay.Dungeon.Rooms;

namespace Gameplay.Dungeon.ShopRooms.BasePurchasableItems
{
    public class PurchasableItemsProvider
    {
        private readonly SpecialtyShopRoom _shopRoom;

        public PurchasableItemsProvider(SpecialtyShopRoom shopRoom)
        {
            _shopRoom = shopRoom;
        }

        public bool AnyItemsSold()
        {
            return _shopRoom.Config.ItemsForSale.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> GetSellableItems()
        {
            return CreateItemsList(_shopRoom.Config.ItemsForSale);
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