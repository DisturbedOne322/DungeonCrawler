using Gameplay.Buffs.Core;

namespace Gameplay.Buffs.ReapplyStrategies
{
    public class ReapplyRefreshDurationStrategy : IBuffReapplyStrategy
    {
        public void ReapplyBuff(BaseBuffInstance instance, BaseBuffData data)
        {
            var turns = data.BuffDurationData.TurnDurations;
            instance.TurnDurationLeft = turns;
        }
    }
}