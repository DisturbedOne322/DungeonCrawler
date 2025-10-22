using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.ReapplyStrategies
{
    public class ReapplyRefreshDurationStrategy : IStatusEffectReapplyStrategy
    {
        public void ReapplyStatusEffect(BaseStatusEffectInstance instance, BaseStatusEffectData data)
        {
            var turns = data.StatusEffectDurationData.TurnDurations;
            instance.TurnDurationLeft.Value = turns;
        }
    }
}