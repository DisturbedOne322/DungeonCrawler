using Data;
using Data.Constants;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "RandomEncounterRoomVariantData")]
    public class RandomEncounterRoomVariantData : CombatRoomVariantData
    {
        public override RoomType RoomType => RoomType.RandomEncounter;
    }
}