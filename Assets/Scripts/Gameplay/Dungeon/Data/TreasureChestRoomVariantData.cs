using Data;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "TreasureChestRoomVariantData", menuName = "Gameplay/Dungeon/Data/TreasureChestRoomVariantData")] 
    public class TreasureChestRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.TreasureChest;
    }
}