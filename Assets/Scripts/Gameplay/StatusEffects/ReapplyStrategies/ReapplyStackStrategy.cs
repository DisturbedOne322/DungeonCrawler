using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.ReapplyStrategies
{
    public class ReapplyStackStrategy : IStatusEffectReapplyStrategy
    {
        public void ReapplyStatusEffect(BaseStatusEffectInstance instance, BaseStatusEffectData data)
        {
            var stacksCurrent = instance.Stacks;
            var maxStacks = data.MaxStacks;

            stacksCurrent = Mathf.Min(maxStacks, stacksCurrent + 1);
            instance.Stacks = stacksCurrent;
        }
    }
}