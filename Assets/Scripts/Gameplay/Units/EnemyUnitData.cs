using Data.Constants;
using UnityEngine;

namespace Gameplay.Units
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayUnits + "EnemyUnitData")]
    public class EnemyUnitData : UnitData
    {
        [SerializeField] private int _experienceBonus;

        public int ExperienceBonus => _experienceBonus;
    }
}