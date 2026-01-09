using Data;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "CorridorRoomVariantData", menuName = "Gameplay/Dungeon/Data/CorridorRoomVariantData")]
    public class CorridorRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.Corridor;
    }
}