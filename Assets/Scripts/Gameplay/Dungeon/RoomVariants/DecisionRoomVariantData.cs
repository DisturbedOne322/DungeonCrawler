using Data;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(fileName = "DecisionRoomVariantData", menuName = "Gameplay/Dungeon/Data/DecisionRoomVariantData")]
    public class DecisionRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.Decision;
    }
}