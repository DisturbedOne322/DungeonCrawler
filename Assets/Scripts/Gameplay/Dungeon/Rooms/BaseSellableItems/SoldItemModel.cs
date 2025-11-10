using UniRx;

namespace Gameplay.Dungeon.Rooms.BaseSellableItems
{
    public class SoldItemModel
    {
        public SoldItemModel(ISellableItemData itemData)
        {
            ItemData = itemData;
            AmountLeft = new IntReactiveProperty(itemData.Amount);
        }

        public ISellableItemData ItemData { get; }

        public IntReactiveProperty AmountLeft { get; }

        public void DecreaseAmount(int amount)
        {
            AmountLeft.Value -= amount;
        }
    }
}