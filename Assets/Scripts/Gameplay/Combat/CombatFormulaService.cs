using Gameplay.Combat.Data;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Combat
{
    public class CombatFormulaService
    {
        private const int MaxStatValue = 100;

        public int GetFinalDamageTo(int incomingDamage, IEntity unit, OffensiveSkillData skillData)
        {
            if (skillData.IsPiercing)
                return incomingDamage;
            
            int constitutionStat = unit.UnitStatsData.Constitution.Value;
            float damageReductionModifier = 1 - Mathf.Clamp(constitutionStat, 1, MaxStatValue) * 0.8f / MaxStatValue;
            
            float constitutionReducedDamage = incomingDamage * damageReductionModifier;
            
            int clampedDamage = Mathf.Max(Mathf.RoundToInt(constitutionReducedDamage), 0);
            
            return clampedDamage;
        }

        public float GetFinalCritChance(IEntity unit, OffensiveSkillData skillData)
        {
            float finalCritChance = 0f;
            
            int dex = unit.UnitStatsData.Dexterity.Value;
            int luck = unit.UnitStatsData.Luck.Value;

            float chanceFromDex = Mathf.Clamp(dex, 0, MaxStatValue) * 0.2f / MaxStatValue;
            float chanceFromLuck = Mathf.Clamp(luck, 0, MaxStatValue) * 0.33f / MaxStatValue;

            finalCritChance = skillData.CritChance + chanceFromDex + chanceFromLuck;
            return Mathf.Clamp01(finalCritChance);
        }

        public float GetFinalEvasionChance(IEntity unit, OffensiveSkillData skillData)
        {
            if(skillData.IsUnavoidable)
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