using Gameplay.Combat.Data;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    public abstract class OffensiveSkill : BaseSkill
    {
        [SerializeField, Min(1)] protected int BaseDamage = 1;
        [SerializeField] protected SkillAnimationData SkillAnimationData;
        
        [SerializeField, Range(0, 1f), Space] private float _baseCritChance = 0;
        [SerializeField] private bool _canCrit = true;
        [SerializeField] private bool _isPiercing = false;
        [SerializeField] private bool _isUnavoidable = false;
        [SerializeField] private DamageType _damageType;

        protected virtual OffensiveSkillData GetSkillData(IEntity entity)
        {
            return new OffensiveSkillData()
            {
                Damage = BaseDamage,
                CritChance = _baseCritChance,
                IsPiercing = _isPiercing,
                IsUnavoidable = _isUnavoidable,
                DamageType = _damageType,
                CanCrit = _canCrit,
            };
        }
    }
}