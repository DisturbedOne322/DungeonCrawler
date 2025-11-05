using System.Collections.Generic;

namespace Gameplay.Dungeon.Rooms.BaseSellableItems
{
    public class SellableItemsProvider
    {
        private readonly BaseSellableItemsConfig _itemsForSaleConfig;

        public SellableItemsProvider(BaseSellableItemsConfig itemsForSaleConfig)
        {
            _itemsForSaleConfig = itemsForSaleConfig;
        }
        
        public bool AnyItemsSold() => _itemsForSaleConfig.ItemsForSale.Count > 0;

        public IReadOnlyList<SoldItemModel> GetSellableItems() => 
            CreateItemsList(_itemsForSaleConfig.ItemsForSale);
        
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