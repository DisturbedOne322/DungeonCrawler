using Gameplay.Combat.Data;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat.Skills
{
    [CreateAssetMenu(fileName = "BaseAttackSkill", menuName = "Gameplay/Skills/BaseAttackSkill")]
    public class BaseAttackSkill : OffensiveSkill
    {
        [SerializeField] private float _strengthScaling = 1;

        public override bool CanUse(CombatService combatService) => true;

        protected override SkillData GetSkillData(IEntity entity)
        {
            var skillData = base.GetSkillData(entity);
            
            int strength = entity.UnitStatsData.Strength.Value;
            int additionalPower = Mathf.RoundToInt(_strengthScaling * strength);
            
            int finalAttackPower = skillData.Damage + additionalPower;
            skillData.Damage = finalAttackPower;
            
            return skillData;
        }
    }
}