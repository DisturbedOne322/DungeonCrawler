using Data.Constants;
using Gameplay.Combat.AI;
using UnityEngine;

namespace Gameplay.Units
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayUnits + "EnemyUnitData")]
    public class EnemyUnitData : UnitData
    {
        [SerializeField] private int _experienceBonus;
        [SerializeField] private AIConfig _aiConfig;
        
        public int ExperienceBonus => _experienceBonus;
        public AIConfig AIConfig => _aiConfig;
    }
}