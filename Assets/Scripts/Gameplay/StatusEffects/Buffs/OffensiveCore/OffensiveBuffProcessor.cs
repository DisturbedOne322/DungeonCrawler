using System.Collections.Generic;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Core;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public class OffensiveBuffProcessor : BaseBuffProcessor<OffensiveBuffData, OffensiveBuffInstance>
    {
        protected override ReactiveCollection<OffensiveBuffData> GetBuffData(IEntity buffTarget)
        {
            return buffTarget.UnitBuffsData.OffensiveBuffs;
        }

        protected override List<OffensiveBuffInstance> GetActiveBuffs(IEntity buffTarget)
        {
            return buffTarget.UnitActiveBuffsData.ActiveOffensiveBuffs;
        }

        protected override OffensiveBuffInstance CreateBuffInstance(OffensiveBuffData buffData,
            IEntity buffTarget)
        {
            return OffensiveBuffInstance.Create(buffData);
        }

        protected override void RemoveBuffInstance(IEntity buffTarget,
            OffensiveBuffInstance offensiveBuffInstance)
        {
            buffTarget.UnitActiveBuffsData.ActiveOffensiveBuffs.Remove(offensiveBuffInstance);
        }
    }
}