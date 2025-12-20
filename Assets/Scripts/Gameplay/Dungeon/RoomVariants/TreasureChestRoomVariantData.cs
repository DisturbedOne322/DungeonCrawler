using Data;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "TreasureChestRoomVariantData", menuName = "Gameplay/Dungeon/Data/TreasureChestRoomVariantData")] 
    public class TreasureChestRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.TreasureChest;
        
        public override void ApplyToRoom(DungeonRoom room)
        {
            var treasureRoom = room as TreasureChestRoom;
            treasureRoom?.SetData(this);
        }
    }
}