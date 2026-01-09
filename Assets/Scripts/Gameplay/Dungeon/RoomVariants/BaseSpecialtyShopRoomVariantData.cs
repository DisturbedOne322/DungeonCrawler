using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;

namespace Gameplay.Dungeon.RoomVariants
{
    public abstract class BaseSpecialtyShopRoomVariantData : RoomVariantData
    {
        public abstract BasePurchasableItemsConfig Config { get; }
    }
}