using Gameplay.Dungeon.RoomVariants;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;

namespace Gameplay.Dungeon.Rooms
{
    public class ShrineRoom : SinglePurchaseRoom
    {
        private ShrineRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;
        public override BasePurchasableItemsConfig Config => _roomData.Config;

        public void SetData(ShrineRoomVariantData data) => _roomData = data;
    }
}