using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "Elite Fight Room Variant Data")]
    public class EliteFightRoomVariantData : CombatRoomVariantData
    {
        public override RoomType RoomType => RoomType.EliteFight;
    }
}