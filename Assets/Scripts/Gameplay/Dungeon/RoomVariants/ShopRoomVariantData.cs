using Data;
using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.ShopRooms.Shop;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "ShopRoomVariantData", menuName = "Gameplay/Dungeon/Data/ShopRoomVariantData")] 
    public class ShopRoomVariantData : RoomVariantData
    {
        [SerializeField] private ShopItemsConfig _config;
        public ShopItemsConfig Config => _config;
        
        public override RoomType RoomType => RoomType.Shop;
        
        public override void ApplyToRoom(DungeonRoom room)
        {
            var shopRoom = room as ShopRoom;
            shopRoom?.SetData(this);
        }
    }
}