using System.Collections.Generic;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Core;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.DefensiveCore
{
    public class DefensiveBuffProcessor : BaseBuffProcessor<DefensiveBuffData, DefensiveBuffInstance>
    {
        protected override ReactiveCollection<DefensiveBuffData> GetBuffData(IEntity buffTarget)
        {
            return buffTarget.UnitBuffsData.DefensiveBuffs;
        }

        protected override List<DefensiveBuffInstance> GetActiveBuffs(IEntity buffTarget)
        {
            return buffTarget.UnitActiveBuffsData.ActiveDefensiveBuffs;
        }

        protected override DefensiveBuffInstance CreateBuffInstance(DefensiveBuffData buffData,
            IEntity buffTarget)
        {
            return DefensiveBuffInstance.Create(buffData);
        }

        protected override void RemoveBuffInstance(IEntity buffTarget,
            DefensiveBuffInstance defensiveBuffInstance)
        {
            buffTarget.UnitActiveBuffsData.ActiveDefensiveBuffs.Remove(defensiveBuffInstance);
        }
    }
}