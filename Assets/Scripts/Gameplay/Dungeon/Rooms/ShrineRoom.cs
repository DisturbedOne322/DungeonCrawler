using Gameplay.Dungeon.Data;
using Gameplay.Dungeon.Rooms.BasePurchasableItems;

namespace Gameplay.Dungeon.RoomTypes
{
    public class ShrineRoom : SinglePurchaseRoom
    {
        private ShrineRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;
        public override BasePurchasableItemsConfig Config => _roomData.Config;

        public void SetData(ShrineRoomVariantData data) => _roomData = data;
    }
}