using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.ReapplyStrategies
{
    public class ReapplyLowerDurationStrategy : IStatusEffectReapplyStrategy
    {
        public void ReapplyStatusEffect(BaseStatusEffectInstance instance, BaseStatusEffectData data)
        {
            instance.TurnDurationLeft--;
        }
    }
}