using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonRoomsData + "Boss Fight Room Variant Data")]
    public class BossFightRoomVariantData : CombatRoomVariantData
    {
        public override RoomType RoomType => RoomType.BossFight;
    }
}