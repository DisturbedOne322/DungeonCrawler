using System.Collections.Generic;
using Data.Constants;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using Gameplay.Skills.Core;
using Helpers;
using UnityEngine;

namespace Gameplay.Skills.OffensiveSkills
{
    [CreateAssetMenu(menuName = MenuPaths.GameplaySkills + "StatScalingSkill")]
    public class StatScalingSkill : OffensiveSkill
    {
        [SerializeField] private List<SkillScalingData> _scalingsList;

        public override SkillData GetSkillData(IGameUnit entity)
        {
            var skillData = base.GetSkillData(entity);

            float additionalPower = 0;

            foreach (var scalingData in _scalingsList)
            {
                var stat = scalingData.StatType;
                var scaling = scalingData.Scaling;

                var statValue = UnitStatsHelper.GetStatValue(entity, stat);
                additionalPower += statValue * scaling;
            }

            var finalAttackPower = skillData.BaseDamage + Mathf.RoundToInt(additionalPower);

            skillData.BaseDamage = finalAttackPower;

            return skillData;
        }
    }
}