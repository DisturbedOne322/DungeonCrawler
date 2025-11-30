using Data;
using Gameplay.Dungeon.Data;

namespace Gameplay.Dungeon.RoomTypes
{
    public class MagicMasterRoom : SinglePurchaseRoom
    {
        private MagicMasterRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;
        
        public void SetData(MagicMasterRoomVariantData data) => _roomData = data;
    }
}