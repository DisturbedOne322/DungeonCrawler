using Gameplay.Dungeon.Data;

namespace Gameplay.Dungeon.RoomTypes
{
    public class ShrineRoom : SinglePurchaseRoom
    {
        private ShrineRoomVariantData _roomData;
        public override RoomVariantData RoomData => _roomData;
        
        public void SetData(ShrineRoomVariantData data) => _roomData = data;
    }
}