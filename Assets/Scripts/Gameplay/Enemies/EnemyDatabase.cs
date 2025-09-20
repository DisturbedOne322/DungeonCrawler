using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyDatabase")]
    public class EnemyDatabase : ScriptableObject
    {
        [SerializeField] private List<UnitData> _database;
        public List<UnitData> Database => _database;
    }
}