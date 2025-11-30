using Data;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;

namespace Gameplay.Dungeon.Data
{
    [CreateAssetMenu(fileName = "CorridorRoomVariantData", menuName = "Gameplay/Dungeon/Data/CorridorRoomVariantData")]
    public class CorridorRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.Corridor;
    }
}