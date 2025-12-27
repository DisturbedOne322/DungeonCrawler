using System;
using Data;
using Gameplay.Facades;

namespace Helpers
{
    public static class UnitStatsHelper
    {
        public static float GetStatValue(IGameUnit entity, StatType statType)
        {
            if (TryGetBaseStatValue(entity, statType, out var baseValue)) return baseValue;
            if (TryGetBonusStatValue(entity, statType, out var bonusValue)) return bonusValue;
            if (TryGetSpecialStatValue(entity, statType, out var specialValue)) return specialValue;

            throw new Exception($"{statType} is unhandled");
        }

        private static bool TryGetBaseStatValue(IGameUnit entity, StatType statType, out float value)
        {
            var stats = entity.UnitStatsData;
            value = 0f;

            switch (statType)
            {
                case StatType.Constitution:
                    value = stats.ConstitutionProp.Value;
                    return true;
                case StatType.Strength:
                    value = stats.StrengthProp.Value;
                    return true;
                case StatType.Dexterity:
                    value = stats.DexterityProp.Value;
                    return true;
                case StatType.Intelligence:
                    value = stats.IntelligenceProp.Value;
                    return true;
                case StatType.Luck:
                    value = stats.LuckProp.Value;
                    return true;
            }

            return false;
        }

        private static bool TryGetBonusStatValue(IGameUnit entity, StatType statType, out float value)
        {
            var bonus = entity.UnitBonusStatsData;
            value = 0f;

            switch (statType)
            {
                case StatType.CriticalChance:
                    value = bonus.CritChanceBonus.Value;
                    return true;
                case StatType.CriticalDamage:
                    value = bonus.CritDamageBonus.Value;
                    return true;
                case StatType.HealthRegen:
                    value = bonus.HealthRegenBonus.Value;
                    return true;
                case StatType.ManaRegen:
                    value = bonus.ManaRegenBonus.Value;
                    return true;
                case StatType.PenetrationRatio:
                    value = bonus.PenetrationRatio.Value;
                    return true;
            }

            return false;
        }

        private static bool TryGetSpecialStatValue(IEntity entity, StatType statType, out float value)
        {
            value = 0f;

            switch (statType)
            {
                case StatType.Health:
                    value = entity.UnitHealthData.CurrentHealth.Value;
                    return true;
                case StatType.Mana:
                    value = entity.UnitManaData.CurrentMana.Value;
                    return true;
            }

            return false;
        }
    }
}