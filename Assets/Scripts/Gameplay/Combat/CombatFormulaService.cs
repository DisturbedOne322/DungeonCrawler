using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Combat
{
    public class CombatFormulaService
    {
        private const int MaxStatValue = 100;

        public int GetFinalDamageTo(GameUnit unit, int rawDamage, bool isPiercing = false)
        {
            if (isPiercing)
                return rawDamage;
            
            int constitutionStat = unit.UnitStatsData.Constitution.Value;
            float damageReductionModifier = 1 - Mathf.Clamp(constitutionStat, 1, MaxStatValue) * 1f / MaxStatValue;
            
            float constitutionReducedDamage = rawDamage * damageReductionModifier;

            if (unit.UnitBuffsData.Guarded.Value)
                constitutionReducedDamage /= 2;
            
            return Mathf.RoundToInt(constitutionReducedDamage);
        }

        public float GetFinalCritChance(GameUnit unit, float skillCritChance = 0)
        {
            float finalCritChance = 0f;
            
            int dex = unit.UnitStatsData.Dexterity.Value;
            int luck = unit.UnitStatsData.Luck.Value;

            float chanceFromDex = dex * 0.2f / MaxStatValue;
            float chanceFromLuck = luck * 0.33f / MaxStatValue;

            finalCritChance = skillCritChance + chanceFromDex + chanceFromLuck;
            return Mathf.Clamp01(finalCritChance);
        }

        public float GetFinalEvasionChance(GameUnit unit)
        {
            int dex = unit.UnitStatsData.Dexterity.Value;
            int luck = unit.UnitStatsData.Luck.Value;

            float chanceFromDex = dex * 0.2f / MaxStatValue;
            float chanceFromLuck = luck * 0.15f / MaxStatValue;

            float finalEvasionChance = chanceFromDex + chanceFromLuck;
            
            return Mathf.Clamp01(finalEvasionChance);
        }
    }
}