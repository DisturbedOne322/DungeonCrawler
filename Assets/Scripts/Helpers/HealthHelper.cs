using Gameplay.Combat.Data.Events;
using Gameplay.Facades;
using UnityEngine;

namespace Helpers
{
    public static class HealthHelper
    {
        private const float MediumHealthThreshold = 0.9f;
        private const float CriticalHealthThreshold = 0.2f;

        public static bool DroppedBelowMediumHealth(HitEventData data)
        {
            var previousHealthPercent = data.HealthPercentBeforeHit;
            var curHealthPercent = GetHealthPercent(data.Target);

            var wasAtMedium = IsAtMediumThreshold(previousHealthPercent);
            var isAtMedium = IsAtMediumThreshold(curHealthPercent);

            return !wasAtMedium && isAtMedium;
        }

        public static bool DroppedBelowCriticalHealth(HitEventData data)
        {
            var previousHealthPercent = data.HealthPercentBeforeHit;
            var curHealthPercent = GetHealthPercent(data.Target);

            var wasAtCritical = IsAtCriticalThreshold(previousHealthPercent);
            var isAtCritical = IsAtCriticalThreshold(curHealthPercent);

            return !wasAtCritical && isAtCritical;
        }

        public static float GetHealthPercent(IEntity entity)
        {
            var currentHp = entity.UnitHealthData.CurrentHealth.Value;
            var maxHp = entity.UnitHealthData.MaxHealth.Value;

            return currentHp * 1f / maxHp;
        }

        public static float GetHealthIncreasePercent(IEntity entity, int flatIncrease)
        {
            int maxHp = entity.UnitHealthData.MaxHealth.Value;
            float increasePercent = flatIncrease * 1f / maxHp;
            
            return GetHealthIncreasePercent(entity, increasePercent);
        }
        
        public static float GetHealthIncreasePercent(IEntity entity, float increasePercent)
        {
            var currentHealthPercent = GetHealthPercent(entity);
            
            float missingHealthPercent = 1 - currentHealthPercent;
            
            increasePercent = Mathf.Clamp(increasePercent, 0, missingHealthPercent);
            
            return increasePercent;
        }

        public static bool IsAtMediumThreshold(float hpPercent)
        {
            return IsBelowThreshold(hpPercent, MediumHealthThreshold);
        }

        public static bool IsAtCriticalThreshold(float hpPercent)
        {
            return IsBelowThreshold(hpPercent, CriticalHealthThreshold);
        }

        private static bool IsBelowThreshold(float percent, float threshold)
        {
            return percent <= threshold;
        }
    }
}