using System.Collections.Generic;
using Gameplay.Dungeon.Rooms;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    public abstract class CombatRoomVariantData : RoomVariantData, ICombatRoomData
    {
        [SerializeField] private List<EnemyUnitData> _enemiesSelection;
        
        public IReadOnlyList<EnemyUnitData> EnemiesSelection => _enemiesSelection;
    }
}