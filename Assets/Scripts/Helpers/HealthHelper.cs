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