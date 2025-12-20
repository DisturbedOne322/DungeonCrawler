using Data;
using Gameplay.Dungeon.Data;
using Gameplay.Dungeon.Rooms.BasePurchasableItems;

namespace Gameplay.Dungeon.RoomTypes
{
    public class MagicMasterRoom : SinglePurchaseRoom
    {
        private MagicMasterRoomVariantData _roomData;
        
        public override RoomVariantData RoomData => _roomData;
        public override BasePurchasableItemsConfig Config => _roomData.Config;

        public void SetData(MagicMasterRoomVariantData data) => _roomData = data;
    }
}