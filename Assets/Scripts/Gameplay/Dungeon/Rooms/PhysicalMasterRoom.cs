using Gameplay.Dungeon.RoomVariants;
using Gameplay.Dungeon.ShopRooms.BasePurchasableItems;

namespace Gameplay.Dungeon.Rooms
{
    public class PhysicalMasterRoom : SinglePurchaseRoom
    {
        private PhysicalMasterRoomVariantData _roomData;
        
        public override RoomVariantData RoomData => _roomData;
        public override BasePurchasableItemsConfig Config => _roomData.Config;

        public void SetData(PhysicalMasterRoomVariantData data) => _roomData = data;
    }
}