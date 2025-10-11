using System.Collections.Generic;
using Gameplay.Buffs.Core;
using Gameplay.Facades;
using UniRx;

namespace Gameplay.Buffs.DefensiveCore
{
    public class DefensiveBuffProcessor : BaseBuffProcessor<DefensiveBuffData, DefensiveBuffInstance>
    {
        protected override ReactiveCollection<DefensiveBuffData> GetBuffData(IEntity buffTarget) => buffTarget.UnitBuffsData.DefensiveBuffs;

        protected override List<DefensiveBuffInstance> GetActiveBuffs(IEntity buffTarget) => buffTarget.UnitActiveBuffsData.ActiveDefensiveBuffs;

        protected override DefensiveBuffInstance CreateBuffInstance(DefensiveBuffData buffData, IEntity buffTarget) => DefensiveBuffInstance.Create(buffData);

        protected override void RemoveBuffInstance(IEntity buffTarget, DefensiveBuffInstance buffInstance) => buffTarget.UnitActiveBuffsData.ActiveDefensiveBuffs.Remove(buffInstance);
    }
}