using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonRoomsData + "Basic FightRoom Variant Data")]
    public class BasicFightRoomVariantData : CombatRoomVariantData
    {
        public override RoomType RoomType => RoomType.BasicFight;
    }
}