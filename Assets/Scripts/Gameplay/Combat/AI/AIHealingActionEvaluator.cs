using Helpers;
using UnityEngine;

namespace Gameplay.Combat.AI
{
    public class AIHealingActionEvaluator
    {
        private const float HealDangerPower = 0.5f;
        
        public float GetHealingValue(AIContext context, int healAmount)
        {
            float effectiveHealPercent = HealthHelper.GetHealthIncreasePercent(context.ActiveUnit, healAmount);
            return CalculateHealingValue(context, effectiveHealPercent);
        }

        public float GetHealingValue(AIContext context, float healPercent)
        {
            float effectiveHealPercent = HealthHelper.GetHealthIncreasePercent(context.ActiveUnit, healPercent);
            return CalculateHealingValue(context, effectiveHealPercent);
        }
        
        private float CalculateHealingValue(AIContext context, float effectiveHealPercent)
        {
            var thisUnit = context.ActiveUnit;
            var config = context.Config;
            
            float currentHpPercent = HealthHelper.GetHealthPercent(thisUnit);

            float danger = Mathf.Pow(1 - currentHpPercent, HealDangerPower);
            
            float multiplier = config.Defense * config.Intelligence;

            return danger * effectiveHealPercent * multiplier;
        }
    }
}