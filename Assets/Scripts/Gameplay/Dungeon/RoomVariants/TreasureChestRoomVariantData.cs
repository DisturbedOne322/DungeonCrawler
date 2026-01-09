 using Data;
using Gameplay.Dungeon.Rooms;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "TreasureChestRoomVariantData", menuName = "Gameplay/Dungeon/Data/TreasureChestRoomVariantData")] 
    public class TreasureChestRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.TreasureChest;
    }
}