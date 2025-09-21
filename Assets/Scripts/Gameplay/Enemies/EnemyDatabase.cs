using System.Collections.Generic;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyDatabase")]
    public class EnemyDatabase : ScriptableObject
    {
        [SerializeField] private List<UnitData> _database;
        public List<UnitData> Database => _database;
    }
}