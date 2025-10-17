using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.ReapplyStrategies
{
    public class ReapplyExtendDurationStrategy : IStatusEffectReapplyStrategy
    {
        public void ReapplyStatusEffect(BaseStatusEffectInstance instance, BaseStatusEffectData data)
        {
            instance.TurnDurationLeft++;
        }
    }
}