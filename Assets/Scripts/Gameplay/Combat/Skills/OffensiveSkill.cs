using UnityEngine;

namespace Gameplay.Combat.Skills
{
    public abstract class OffensiveSkill : BaseSkill
    {
        [SerializeField, Min(1)] protected int BaseDamage = 1;
        
        protected abstract int GetHitDamage(CombatData combatData);
    }
}