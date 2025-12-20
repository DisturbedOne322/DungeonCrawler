using Gameplay.StatusEffects.Core;
using UnityEngine;

namespace Gameplay.StatusEffects.ReapplyStrategies
{
    public class ReapplyStackStrategy : IStatusEffectReapplyStrategy
    {
        public void ReapplyStatusEffect(BaseStatusEffectInstance instance)
        {
            var stacksCurrent = instance.Stacks.Value;
            var maxStacks = instance.StatusEffectData.MaxStacks;

            stacksCurrent = Mathf.Min(maxStacks, stacksCurrent + 1);
            instance.Stacks.Value = stacksCurrent;
        }
    }
}