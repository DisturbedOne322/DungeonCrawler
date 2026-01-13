using System.Collections.Generic;
using Data;
using Data.Constants;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Dungeon.RoomVariants
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDungeonData + "CombatRoomVariantData")]
    public class CombatRoomVariantData : RoomVariantData
    {
        [SerializeField] private List<EnemyUnitData> _enemiesSelection;
        
        public List<EnemyUnitData> EnemiesSelection => _enemiesSelection;
        
        public override RoomType RoomType => RoomType.Combat;
    }
}