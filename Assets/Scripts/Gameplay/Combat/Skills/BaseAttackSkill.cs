using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "BaseAttackSkill", menuName = "Skills/BaseAttackSkill")]
    public class BaseAttackSkill : OffensiveSkill
    {
        [SerializeField] private float _strengthScaling = 1;
        
        protected override async UniTask PerformAction(CombatService combatService) => await combatService.DealDamageToOtherUnit(GetSkillData(combatService.ActiveUnit.UnitStatsData));

        public override bool CanUse(CombatService combatService) => true;

        protected override OffensiveSkillData GetSkillData(UnitStatsData statsData)
        {
            var skillData = base.GetSkillData(statsData);
            
            int strength = statsData.Strength.Value;
            int additionalPower = Mathf.RoundToInt(_strengthScaling * strength);
            
            int finalAttackPower = BaseDamage + additionalPower;

            skillData.Damage = finalAttackPower;
            return skillData;
        }
    }
}