using Gameplay.Dungeon.RoomVariants;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;

namespace Gameplay.Dungeon.Rooms
{
    public class MagicMasterRoom : SinglePurchaseRoom
    {
        private MagicMasterRoomVariantData _roomData;
        
        public override RoomVariantData RoomData => _roomData;
        public override BasePurchasableItemsConfig Config => _roomData.Config;

        public void SetData(MagicMasterRoomVariantData data) => _roomData = data;
    }
}