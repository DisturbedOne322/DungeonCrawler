using System.Collections.Generic;
using Gameplay.Dungeon.Rooms;
using Gameplay.Services;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyFactory
    {
        private readonly ContainerFactory _containerFactory;

        private EnemyFactory(ContainerFactory containerFactory)
        {
            _containerFactory = containerFactory;
        }

        public EnemyUnit CreateEnemy(ICombatRoomData roomData)
        {
            var enemySelection = roomData.EnemiesSelection;
            return SpawnRandomEnemy(enemySelection);
        }
        
        private EnemyUnit SpawnRandomEnemy(IReadOnlyList<EnemyUnitData> enemySelection)
        {
            int enemyIndex = Random.Range(0, enemySelection.Count);
            var enemyData = enemySelection[enemyIndex];
            
            var prefab = enemyData.Prefab.gameObject;
            var unit = _containerFactory.Create<EnemyUnit>(prefab);

            unit.InitializeUnit(enemyData);
            unit.SetExperienceBonus(enemyData.ExperienceBonus);
            return unit;
        }
    }
}