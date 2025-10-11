using System;
using Data;
using Gameplay.Facades;

namespace Helpers
{
    public static class UnitStatsHelper
    {
        public static int GetStatValue(IEntity entity, StatType statType)
        {
            var health = entity.UnitHealthData.CurrentHealth;
            var statsData = entity.UnitStatsData;
            
            switch (statType)
            {
                case StatType.Health:
                    return health.Value;
                case StatType.Mana:
                    return 0;
                case StatType.Constitution:
                    return statsData.Constitution.Value;
                case StatType.Strength:
                    return statsData.Strength.Value;
                case StatType.Dexterity:
                    return statsData.Dexterity.Value;
                case StatType.Intelligence:
                    return statsData.Intelligence.Value;
                case StatType.Luck:
                    return statsData.Luck.Value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(statType), statType, null);
            }
        }
    }
}