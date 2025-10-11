using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using Gameplay.Skills.Core;
using Helpers;
using UnityEngine;

namespace Gameplay.Skills.OffensiveSkills
{
    [CreateAssetMenu(fileName = "StatScalingSkill", menuName = "Gameplay/Skills/StatScalingSkill")]
    public class StatScalingSkill : OffensiveSkill
    {
        [SerializeField] private List<SkillScalingData> _scalingsList;

        protected override SkillData GetSkillData(IEntity entity)
        {
            var skillData = base.GetSkillData(entity);

            float additionalPower = 0;

            foreach (var scalingData in _scalingsList)
            {
                var stat = scalingData.StatType;
                float scaling = scalingData.Scaling;
                
                var statValue = UnitStatsHelper.GetStatValue(entity, stat);
                additionalPower += statValue * scaling;
            }
            
            int finalAttackPower = skillData.BaseDamage + Mathf.RoundToInt(additionalPower);
        
            skillData.BaseDamage = finalAttackPower;
            
            return skillData;
        }
    }
}