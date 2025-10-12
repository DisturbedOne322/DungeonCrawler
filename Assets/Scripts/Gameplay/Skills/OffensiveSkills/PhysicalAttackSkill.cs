using Cysharp.Threading.Tasks;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.Skills.Core;
using UnityEngine;

namespace Gameplay.Skills.OffensiveSkills
{
    [CreateAssetMenu(fileName = "PhysicalAttackSkill", menuName = "Gameplay/Skills/PhysicalAttackSkill")]
    public class PhysicalAttackSkill : OffensiveSkill
    {
        [SerializeField, Space] private float _strengthScaling = 1.25f;
        [SerializeField, Space] private SkillData _selfDamageData;
        
        protected override async UniTask PerformAction(CombatService combatService)
        {
            var stream = combatService.CreateFinalHitsStream(_selfDamageData);
            combatService.DealDamageToActiveUnit(stream, 0);
            
            await base.PerformAction(combatService);
        }

        public override bool CanUse(CombatService combatService) => combatService.ActiveUnit.UnitHealthController.HasEnoughHp(_selfDamageData.BaseDamage);

        protected override SkillData GetSkillData(IEntity entity)
        {
            var skillData = base.GetSkillData(entity);
            int strength = entity.UnitStatsData.Strength.Value;
            int additionalPower = Mathf.RoundToInt(_strengthScaling * strength);
            
            int finalAttackPower = skillData.BaseDamage + additionalPower;
        
            skillData.BaseDamage = finalAttackPower;
            return skillData;
        }
    }
}