using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    public abstract class OffensiveSkill : BaseSkill
    {
        [SerializeField] protected int BaseDamage = 1;
        [SerializeField] private float _baseCritChance = 0;
        [SerializeField] private bool _isPiercing = false;
        [SerializeField] private bool _isUnavoidable = false;
        [SerializeField] private DamageType _damageType;
        

        protected virtual OffensiveSkillData GetSkillData(UnitStatsData statsData)
        {
            return new OffensiveSkillData()
            {
                Damage = BaseDamage,
                CritChance = _baseCritChance,
                IsPiercing = _isPiercing,
                IsUnavoidable = _isUnavoidable,
                DamageType = _damageType,
            };
        }
    }
}