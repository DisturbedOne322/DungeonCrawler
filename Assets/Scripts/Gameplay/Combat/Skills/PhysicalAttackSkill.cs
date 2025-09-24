using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "PhysicalAttackSkill", menuName = "Skills/PhysicalAttackSkill")]
    public class PhysicalAttackSkill : OffensiveSkill
    {
        [SerializeField] private float _strengthScaling = 1.25f;
        [SerializeField] private int _fixedHealthPrice = 5;
        
        public override async UniTask UseAction(CombatService service)
        {
            await UniTask.WaitForSeconds(0.5f);
            
            service.DealDamageToActiveUnit(GetSelfDamageData());
            service.DealDamageToOtherUnit(GetSkillData(service.ActiveUnit.UnitStatsData));
            
            await UniTask.WaitForSeconds(0.5f);
        }

        public override bool CanUse(CombatService combatService)
        {
            int currentHealth = combatService.ActiveUnit.HealthData.CurrentHealth.Value;
            return currentHealth > _fixedHealthPrice;
        }

        protected override OffensiveSkillData GetSkillData(UnitStatsData statsData)
        {
            var skillData = base.GetSkillData(statsData);
            int strength = statsData.Strength.Value;
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
                DamageType = DamageType.Physical
            };
        }
    }
}