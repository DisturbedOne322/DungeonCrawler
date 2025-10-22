using Gameplay.Combat.Data.Events;
using Gameplay.Facades;

namespace Helpers
{
    public static class HealthHelper
    {
        private const float MediumHealthThreshold = 0.9f;
        private const float CriticalHealthThreshold = 0.2f;

        public static bool DroppedBelowMediumHealth(HitEventData data)
        {
            float previousHealthPercent = data.HealthPercentBeforeHit;
            float curHealthPercent = GetHealthPercent(data.Target);

            bool wasAtMedium = IsAtMediumThreshold(previousHealthPercent);
            bool isAtMedium = IsAtMediumThreshold(curHealthPercent);
            
            return !wasAtMedium && isAtMedium;
        }
        
        public static bool DroppedBelowCriticalHealth(HitEventData data)
        {
            float previousHealthPercent = data.HealthPercentBeforeHit;
            float curHealthPercent = GetHealthPercent(data.Target);

            bool wasAtCritical = IsAtCriticalThreshold(previousHealthPercent);
            bool isAtCritical = IsAtCriticalThreshold(curHealthPercent);
            
            return !wasAtCritical && isAtCritical;
        }
        
        public static float GetHealthPercent(IEntity entity)
        {
            var currentHp = entity.UnitHealthData.CurrentHealth.Value;
            var maxHp = entity.UnitHealthData.MaxHealth.Value;
            
            return currentHp * 1f / maxHp;
        }
        
        public static bool IsAtMediumThreshold(float hpPercent) => IsBelowThreshold(hpPercent, MediumHealthThreshold);
        public static bool IsAtCriticalThreshold(float hpPercent) => IsBelowThreshold(hpPercent, CriticalHealthThreshold);
        
        private static bool IsBelowThreshold(float percent, float threshold) => percent <= threshold;
    }
}