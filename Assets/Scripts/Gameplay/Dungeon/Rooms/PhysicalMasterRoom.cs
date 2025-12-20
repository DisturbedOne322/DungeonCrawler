using Data;
using Gameplay.Dungeon.Data;
using Gameplay.Dungeon.Rooms.BasePurchasableItems;

namespace Gameplay.Dungeon.RoomTypes
{
    public class PhysicalMasterRoom : SinglePurchaseRoom
    {
        private PhysicalMasterRoomVariantData _roomData;
        
        public override RoomVariantData RoomData => _roomData;
        public override BasePurchasableItemsConfig Config => _roomData.Config;

        public void SetData(PhysicalMasterRoomVariantData data) => _roomData = data;
    }
}