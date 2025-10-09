using System.Collections.Generic;
using Gameplay.Buffs.Core;
using Gameplay.Facades;
using UniRx;

namespace Gameplay.Buffs.StatBuffsCore
{
    public class StatBuffProcessor : BaseBuffProcessor<StatBuffData, StatBuffInstance>
    {
        protected override ReactiveCollection<StatBuffData> GetBuffData(IEntity buffTarget) => buffTarget.UnitBuffsData.StatBuffs;

        protected override List<StatBuffInstance> GetActiveBuffs(IEntity buffTarget) => buffTarget.UnitActiveBuffsData.ActiveStatBuffs;

        protected override StatBuffInstance CreateBuffInstance(StatBuffData buffData, IEntity buffTarget) => buffData.CreateBuffInstance(buffTarget);
        
        protected override void RemoveBuffInstance(IEntity buffTarget, StatBuffInstance buffInstance)
        {
            buffInstance.StatBuffData.RemoveFrom(buffTarget, buffInstance);
            buffTarget.UnitActiveBuffsData.ActiveStatBuffs.Remove(buffInstance);
        }
    }
}