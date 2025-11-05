using UniRx;

namespace Gameplay.Dungeon.Rooms.BaseSellableItems
{
    public class SoldItemModel
    {
        private readonly IntReactiveProperty _amountLeft;

        public ISellableItemData ItemData { get; }

        public IntReactiveProperty AmountLeft => _amountLeft;

        public SoldItemModel(ISellableItemData itemData)
        {
            ItemData = itemData;
            _amountLeft = new(itemData.Amount);
        }
        
        public void DecreaseAmount(int amount) => _amountLeft.Value -= amount;
    }
}