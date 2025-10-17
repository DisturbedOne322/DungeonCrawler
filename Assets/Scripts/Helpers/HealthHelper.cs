using Gameplay.Facades;

namespace Helpers
{
    public static class HealthHelper
    {
        private const float MediumHealthThreshold = 0.5f;
        private const float CriticalHealthThreshold = 0.2f;
        
        public static bool IsAtMediumHealth(IEntity entity)=> IsBelowThreshold(entity, MediumHealthThreshold);
        
        public static bool IsAtCriticalHealth(IEntity entity) => IsBelowThreshold(entity, CriticalHealthThreshold);

        private static bool IsBelowThreshold(IEntity entity, float threshold)
        {
            var currentHp = entity.UnitHealthData.CurrentHealth.Value;
            var maxHp = entity.UnitHealthData.MaxHealth.Value;

            return currentHp * 1f / maxHp < threshold;
        }
    }
}