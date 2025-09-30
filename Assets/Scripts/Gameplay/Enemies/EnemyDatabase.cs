using System.Collections.Generic;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Gameplay/Enemies/EnemyDatabase")]
    public class EnemyDatabase : ScriptableObject
    {
        [SerializeField] private List<EnemyUnitData> _database;
        public List<EnemyUnitData> Database => _database;
    }
}