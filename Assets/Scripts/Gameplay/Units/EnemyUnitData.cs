using UnityEngine;

namespace Gameplay.Units
{
    [CreateAssetMenu(fileName = "EnemyUnitData", menuName = "Gameplay/Units/EnemyUnitData")]
    public class EnemyUnitData : UnitData
    {
        [SerializeField] private int _experienceBonus;

        public int ExperienceBonus => _experienceBonus;
    }
}