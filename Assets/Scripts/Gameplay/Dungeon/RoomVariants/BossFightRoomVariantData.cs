using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "Boss Fight Room Variant Data")]
    public class BossFightRoomVariantData : CombatRoomVariantData
    {
        public override RoomType RoomType => RoomType.BossFight;
    }
}