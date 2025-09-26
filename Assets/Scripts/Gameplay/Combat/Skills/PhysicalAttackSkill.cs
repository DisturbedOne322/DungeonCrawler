using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "PhysicalAttackSkill", menuName = "Skills/PhysicalAttackSkill")]
    public class PhysicalAttackSkill : OffensiveSkill
    {
        [SerializeField] private float _strengthScaling = 1.25f;
        [SerializeField] private int _fixedHealthPrice = 5;
        
        protected override async UniTask PerformAction(CombatService service)
        {
            service.DealDamageToActiveUnit(GetSelfDamageData()).Forget();
            await service.DealDamageToOtherUnit(GetSkillData(service.ActiveUnit));
        }

        public override bool CanUse(CombatService combatService)
        {
            int currentHealth = combatService.ActiveUnit.UnitHealthData.CurrentHealth.Value;
            return currentHealth > _fixedHealthPrice;
        }

        protected override OffensiveSkillData GetSkillData(IEntity entity)
        {
            var skillData = base.GetSkillData(entity);
            int strength = entity.UnitStatsData.Strength.Value;
            int additionalPower = Mathf.RoundToInt(_strengthScaling * strength);
            
            int finalAttackPower = BaseDamage + additionalPower;

            skillData.Damage = finalAttackPower;
            return skillData;
        }

        private OffensiveSkillData GetSelfDamageData()
        {
            return new OffensiveSkillData()
            {
                Damage = _fixedHealthPrice,
                IsPiercing = true,
                IsUnavoidable = true,
                CanCrit = false,
                DamageType = DamageType.Absolute
            };
        }
    }
}