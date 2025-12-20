using System.Collections.Generic;
using Gameplay.Dungeon.RoomTypes;

namespace Gameplay.Dungeon.Rooms.BasePurchasableItems
{
    public class PurchasableItemsProvider
    {
        private readonly SinglePurchaseRoom _purchaseRoom;

        public PurchasableItemsProvider(SinglePurchaseRoom purchaseRoom)
        {
            _purchaseRoom = purchaseRoom;
        }

        public bool AnyItemsSold()
        {
            return _purchaseRoom.Config.ItemsForSale.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> GetSellableItems()
        {
            return CreateItemsList(_purchaseRoom.Config.ItemsForSale);
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