using System.Collections.Generic;
using Gameplay.Buffs.Core;
using Gameplay.Facades;
using UniRx;

namespace Gameplay.Buffs.OffensiveCore
{
    public class OffensiveBuffProcessor : BaseBuffProcessor<OffensiveBuffData, OffensiveBuffInstance>
    {
        protected override ReactiveCollection<OffensiveBuffData> GetBuffData(IEntity buffTarget) => buffTarget.UnitBuffsData.OffensiveBuffs;

        protected override List<OffensiveBuffInstance> GetActiveBuffs(IEntity buffTarget) => buffTarget.UnitActiveBuffsData.ActiveOffensiveBuffs;

        protected override OffensiveBuffInstance CreateBuffInstance(OffensiveBuffData buffData, IEntity buffTarget) => OffensiveBuffInstance.Create(buffData);
        
        protected override void RemoveBuffInstance(IEntity buffTarget, OffensiveBuffInstance buffInstance) => buffTarget.UnitActiveBuffsData.ActiveOffensiveBuffs.Remove(buffInstance);
    }
}