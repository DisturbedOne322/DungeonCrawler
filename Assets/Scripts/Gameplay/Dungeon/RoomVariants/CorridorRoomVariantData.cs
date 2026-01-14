using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonRoomsData + "CorridorRoomVariantData")]
    public class CorridorRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.Corridor;
    }
}