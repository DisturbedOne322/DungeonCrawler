using Gameplay.Buffs.Core;
using UnityEngine;

namespace Gameplay.Buffs.ReapplyStrategies
{
    public class ReapplyStackStrategy : IBuffReapplyStrategy
    {
        public void ReapplyBuff(BaseBuffInstance instance, BaseBuffData data)
        {
            int stacksCurrent = instance.Stacks;
            int maxStacks = data.MaxStacks;

            stacksCurrent = Mathf.Min(maxStacks, stacksCurrent + 1);
            instance.Stacks = stacksCurrent;
        }
    }
}