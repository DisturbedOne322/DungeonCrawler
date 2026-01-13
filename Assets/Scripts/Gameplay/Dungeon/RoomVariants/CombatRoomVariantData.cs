using System.Collections.Generic;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    public abstract class CombatRoomVariantData : RoomVariantData
    {
        [SerializeField] private List<EnemyUnitData> _enemiesSelection;
        
        public List<EnemyUnitData> EnemiesSelection => _enemiesSelection;
    }
}