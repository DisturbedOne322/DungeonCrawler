using System.Collections.Generic;

namespace Gameplay.Dungeon.Rooms.BasePurchasableItems
{
    public class PurchasableItemsProvider
    {
        private readonly BasePurchasableItemsConfig _itemsForSaleConfig;

        public PurchasableItemsProvider(BasePurchasableItemsConfig itemsForSaleConfig)
        {
            _itemsForSaleConfig = itemsForSaleConfig;
        }

        public bool AnyItemsSold()
        {
            return _itemsForSaleConfig.ItemsForSale.Count > 0;
        }

        public IReadOnlyList<PurchasedItemModel> GetSellableItems()
        {
            return CreateItemsList(_itemsForSaleConfig.ItemsForSale);
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