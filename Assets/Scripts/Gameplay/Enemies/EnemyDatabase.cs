using System.Collections.Generic;
using Gameplay.Progression;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Enemies
{
    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Gameplay/Enemies/EnemyDatabase")]
    public class EnemyDatabase : GameplayConfig
    {
        [SerializeField] private List<EnemyUnitData> _database;
        public List<EnemyUnitData> Database => _database;
    }
}