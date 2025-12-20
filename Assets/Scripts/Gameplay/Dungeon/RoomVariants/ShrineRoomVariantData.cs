using Data;
using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.ShopRooms.Shrine;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "ShrineRoomVariantData", menuName = "Gameplay/Dungeon/Data/ShrineRoomVariantData")] 
    public class ShrineRoomVariantData : RoomVariantData
    {
        [SerializeField] private ShrineBuffsConfig _config;
        public ShrineBuffsConfig Config => _config;
        
        public override RoomType RoomType => RoomType.Shrine;
        
        public override void ApplyToRoom(DungeonRoom room)
        {
            var shrineRoom = room as ShrineRoom;
            shrineRoom?.SetData(this);
        }
    }
}