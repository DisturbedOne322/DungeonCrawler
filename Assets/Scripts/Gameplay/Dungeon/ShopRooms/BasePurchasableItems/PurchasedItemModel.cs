using UniRx;

namespace Gameplay.Dungeon.ShopRooms.BasePurchasableItems
{
    public class PurchasedItemModel
    {
        public PurchasedItemModel(IPurchasableItemData itemData)
        {
            ItemData = itemData;
            AmountLeft = new IntReactiveProperty(itemData.Amount);
        }

        public IPurchasableItemData ItemData { get; }

        public IntReactiveProperty AmountLeft { get; }

        public void DecreaseAmount(int amount)
        {
            AmountLeft.Value -= amount;
        }
    }
}