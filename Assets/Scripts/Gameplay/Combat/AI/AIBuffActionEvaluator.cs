using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using Helpers;
using UnityEngine;

namespace Gameplay.Combat.AI
{
    public class AIBuffActionEvaluator
    {
        private readonly CombatService _combatService;
        
        public AIBuffActionEvaluator(CombatService combatService)
        {
            _combatService = combatService;
        }

        public float GetDefensiveBuffValue(AIContext context, DefensiveBuffData buffData)
        {
            var enemy = context.OtherUnit;
            var config = context.Config;

            var dangerousBuffs = new List<HitBuffInstance>();

            foreach (var hitBuff in enemy.UnitActiveStatusEffectsContainer.ActiveOffensiveBuffs)
            {
                if(StatusEffectsHelper.IsExpirationTypeActionBased(hitBuff.EffectExpirationType))
                    dangerousBuffs.Add(hitBuff);
            }

            if (dangerousBuffs.Count == 0)
                return 0f;

            HitBuffInstance mostDangerousBuff = null;
            float highestThreat = 0f;

            foreach (var buff in dangerousBuffs)
            {
                float threat = GetBestSkillBonusDamage(enemy, buff.HitBuffData);
                if (threat > highestThreat)
                {
                    highestThreat = threat;
                    mostDangerousBuff = buff;
                }
            }

            if (mostDangerousBuff == null)
                return 0;

            float damageReduction = Mathf.Clamp01(buffData.GetDamageReductionMultiplier(mostDangerousBuff));
            return (1 - damageReduction) * config.Defense * config.Intelligence;
        }
        
        public float GetOffensiveBuffValue(AIContext context, HitBuffData hitBuffData)
        {
            var unit = context.ActiveUnit;
            var enemy = context.OtherUnit;
            var intelligence = context.Config.Intelligence;

            float bestBonusDamage = GetBestSkillBonusDamage(unit, hitBuffData);

            if (enemy.UnitHealthData.MaxHealth.Value <= 0)
                return 0f;

            float normalized = bestBonusDamage / enemy.UnitHealthData.MaxHealth.Value;
            return normalized * intelligence;
        }

        private float GetBestSkillBonusDamage(IGameUnit unit, HitBuffData hitBuffData)
        {
            float bestBonus = 0f;

            foreach (var skill in unit.UnitSkillsContainer.Skills)
            {
                if(skill is not OffensiveSkill offSkill)
                    continue;
                
                if (!skill.CanUse(_combatService))
                    continue;

                float bonus = CalculateSkillBonusDamage(unit, offSkill, hitBuffData);
                bestBonus = Mathf.Max(bestBonus, bonus);
            }

            return bestBonus;
        }

        private float CalculateSkillBonusDamage(IGameUnit unit, OffensiveSkill skill, HitBuffData hitBuffData)
        {
            var skillData = skill.GetSkillData(unit);
            int expectedHits = Mathf.RoundToInt((skillData.MinHits + skillData.MaxHits) * 0.5f);

            float bonusDamage = 0f;

            for (int hitIndex = 0; hitIndex < expectedHits; hitIndex++)
            {
                var hitData = new HitData(skillData, hitIndex);
                float multiplier = hitBuffData.GetExpectedDamageMultiplier(hitData);

                if (multiplier > 1f) 
                    bonusDamage += skillData.BaseDamage * (multiplier - 1f);
            }

            return bonusDamage;
        }
    }
}