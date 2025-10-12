using Gameplay.Buffs.Core;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat.Services
{
    public class CombatFormulaService
    {
        private const int MaxStatValue = 100;

        public void ReduceDamageByStats(IEntity unit, in DamageContext damageContext)
        {
            if (damageContext.SkillData.IsPiercing)
                return;
            
            int constitutionStat = unit.UnitStatsData.Constitution.Value;
            float damageReductionModifier = 1 - Mathf.Clamp(constitutionStat, 1, MaxStatValue) * 0.8f / MaxStatValue;
            
            float constitutionReducedDamage = damageContext.HitData.Damage * damageReductionModifier;
            
            int clampedDamage = Mathf.Max(Mathf.RoundToInt(constitutionReducedDamage), 0);
            
            damageContext.HitData.Damage = clampedDamage;
        }

        public float GetFinalCritChance(IEntity unit, in DamageContext damageContext)
        {
            if(!damageContext.SkillData.CanCrit)
                return -1;
            
            float finalCritChance = 0f;
            
            int dex = unit.UnitStatsData.Dexterity.Value;
            int luck = unit.UnitStatsData.Luck.Value;

            float chanceFromDex = Mathf.Clamp(dex, 0, MaxStatValue) * 0.2f / MaxStatValue;
            float chanceFromLuck = Mathf.Clamp(luck, 0, MaxStatValue) * 0.33f / MaxStatValue;

            finalCritChance = damageContext.SkillData.BaseCritChance + chanceFromDex + chanceFromLuck;
            return Mathf.Clamp01(finalCritChance);
        }

        public float GetFinalEvasionChance(IEntity unit, in DamageContext damageContext)
        {
            if(damageContext.SkillData.IsUnavoidable)
                return 0f;
            
            int dex = unit.UnitStatsData.Dexterity.Value;
            int luck = unit.UnitStatsData.Luck.Value;

            float chanceFromDex = Mathf.Clamp(dex, 0, MaxStatValue) * 0.2f / MaxStatValue;
            float chanceFromLuck = Mathf.Clamp(luck, 0, MaxStatValue) * 0.15f / MaxStatValue;

            float finalEvasionChance = chanceFromDex + chanceFromLuck;
            
            return Mathf.Clamp01(finalEvasionChance);
        }
    }
}