 using Data;
 using Data.Constants;
 using Gameplay.Dungeon.Rooms;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "TreasureChestRoomVariantData")] 
    public class TreasureChestRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.TreasureChest;
    }
}