using System;
using UniRx;

namespace Gameplay.Shop
{
    public class ShopItemModel
    {
        private readonly IntReactiveProperty _amountLeft;

        public IShopItemData ItemData { get; }

        public IObservable<int> AmountLeft => _amountLeft;

        public ShopItemModel(IShopItemData itemData)
        {
            ItemData = itemData;
            _amountLeft = new(itemData.Amount);
        }
        
        public void DecreaseAmount(int amount) => _amountLeft.Value -= amount;
    }
}