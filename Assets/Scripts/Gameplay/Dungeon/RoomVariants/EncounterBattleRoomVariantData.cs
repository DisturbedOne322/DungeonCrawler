using System.Collections.Generic;
using Data;
using Data.Constants;
using Gameplay.Dungeon.Rooms;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "Encounter Battle Room Variant Data")]
    public class EncounterBattleRoomVariantData : EncounterRoomVariantData, ICombatRoomData
    {
        [SerializeField] private List<EnemyUnitData> _enemiesSelection;
        public override RoomType RoomType => RoomType.EncounterBattle;
        public IReadOnlyList<EnemyUnitData> EnemiesSelection => _enemiesSelection;
    }
}