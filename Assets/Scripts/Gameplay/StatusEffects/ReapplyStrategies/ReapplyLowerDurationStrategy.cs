using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.ReapplyStrategies
{
    public class ReapplyLowerDurationStrategy : IStatusEffectReapplyStrategy
    {
        public void ReapplyStatusEffect(BaseStatusEffectInstance instance, BaseStatusEffectData data)
        {
            instance.DurationLeft.Value--;
        }
    }
}