using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.ReapplyStrategies
{
    public class ReapplyRefreshDurationStrategy : IStatusEffectReapplyStrategy
    {
        public void ReapplyStatusEffect(BaseStatusEffectInstance instance)
        {
            var turns = instance.StatusEffectData.StatusEffectDurationData.Duration;
            instance.DurationLeft.Value = turns;
        }
    }
}