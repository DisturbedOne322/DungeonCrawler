namespace Gameplay.Dungeon.Rooms.BasePurchasableItems
{
    public interface IPurchasableItemData
    {
        BaseGameItem Item { get; }
        int Price { get; }
        int Amount { get; }
    }
}