using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "DecisionRoomVariantData")]
    public class DecisionRoomVariantData : RoomVariantData
    {
        public override RoomType RoomType => RoomType.Decision;
    }
}