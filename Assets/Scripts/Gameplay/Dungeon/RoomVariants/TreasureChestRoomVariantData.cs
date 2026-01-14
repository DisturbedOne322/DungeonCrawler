using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonRoomsData + "TreasureChestRoomVariantData")]
    public class TreasureChestRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.TreasureChest;
    }
}