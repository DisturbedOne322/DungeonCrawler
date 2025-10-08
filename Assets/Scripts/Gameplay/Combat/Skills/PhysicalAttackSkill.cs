using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "PhysicalAttackSkill", menuName = "Gameplay/Skills/PhysicalAttackSkill")]
    public class PhysicalAttackSkill : OffensiveSkill
    {
        [SerializeField] private float _strengthScaling = 1.25f;
        [SerializeField] private int _fixedHealthPrice = 5;
        
        protected override async UniTask PerformAction(CombatService combatService)
        {
            var task = StartAnimation(combatService);

            combatService.DealDamageToActiveUnit(GetSelfDamageData());
            
            await ProcessHit(combatService);

            await task;
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
                DamageType = DamageType.Absolute,
                CanBeBuffed = false
            };
        }
    }
}