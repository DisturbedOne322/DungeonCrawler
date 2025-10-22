using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.ReapplyStrategies
{
    public class ReapplyExtendDurationStrategy : IStatusEffectReapplyStrategy
    {
        public void ReapplyStatusEffect(BaseStatusEffectInstance instance, BaseStatusEffectData data)
        {
            instance.TurnDurationLeft.Value++;
        }
    }
}