namespace Gameplay.Shop
{
    public interface IShopItemData
    {
        BaseGameItem Item { get; }
        int Price { get; }
        int Amount { get; }
    }
}