using System.Collections.Generic;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Core;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public class OffensiveBuffProcessor : BaseBuffProcessor<OffensiveStatusEffectData, OffensiveBuffInstance>
    {
        protected override ReactiveCollection<OffensiveStatusEffectData> GetBuffData(IEntity buffTarget)
        {
            return buffTarget.UnitBuffsData.OffensiveBuffs;
        }

        protected override List<OffensiveBuffInstance> GetActiveBuffs(IEntity buffTarget)
        {
            return buffTarget.UnitActiveBuffsData.ActiveOffensiveBuffs;
        }

        protected override OffensiveBuffInstance CreateBuffInstance(OffensiveStatusEffectData statusEffectData,
            IEntity buffTarget)
        {
            return OffensiveBuffInstance.Create(statusEffectData);
        }

        protected override void RemoveBuffInstance(IEntity buffTarget,
            OffensiveBuffInstance offensiveBuffInstance)
        {
            buffTarget.UnitActiveBuffsData.ActiveOffensiveBuffs.Remove(offensiveBuffInstance);
        }
    }
}