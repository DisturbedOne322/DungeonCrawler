using Data;
using Gameplay.Dungeon.Data;

namespace Gameplay.Dungeon.RoomTypes
{
    public class PhysicalMasterRoom : SinglePurchaseRoom
    {
        private PhysicalMasterRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;
        
        public void SetData(PhysicalMasterRoomVariantData data) => _roomData = data;
    }
}