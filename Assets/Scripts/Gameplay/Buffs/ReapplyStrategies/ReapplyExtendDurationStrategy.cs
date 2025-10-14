using Gameplay.Buffs.Core;

namespace Gameplay.Buffs.ReapplyStrategies
{
    public class ReapplyExtendDurationStrategy : IBuffReapplyStrategy
    {
        public void ReapplyBuff(BaseBuffInstance instance, BaseBuffData data) => instance.TurnDurationLeft++;
    }
}