namespace Gameplay.Dungeon.Rooms.BaseSellableItems
{
    public interface ISellableItemData
    {
        BaseGameItem Item { get; }
        int Price { get; }
        int Amount { get; }
    }
}