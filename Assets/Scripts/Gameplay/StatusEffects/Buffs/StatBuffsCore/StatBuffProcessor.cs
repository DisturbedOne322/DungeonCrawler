using System.Collections.Generic;
using Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Core;
using UniRx;

namespace Gameplay.StatusEffects.Buffs.StatBuffsCore
{
    public class StatBuffProcessor : BaseBuffProcessor<StatBuffData, StatBuffInstance>
    {
        private readonly UnitStatsModificationService _unitStatsModificationService;

        public StatBuffProcessor(UnitStatsModificationService unitStatsModificationService)
        {
            _unitStatsModificationService = unitStatsModificationService;
        }

        protected override ReactiveCollection<StatBuffData> GetBuffData(IEntity buffTarget)
        {
            return buffTarget.UnitBuffsData.StatBuffs;
        }

        protected override List<StatBuffInstance> GetActiveBuffs(IEntity buffTarget)
        {
            return buffTarget.UnitActiveBuffsData.ActiveStatBuffs;
        }

        protected override StatBuffInstance CreateBuffInstance(StatBuffData buffData,
            IEntity buffTarget)
        {
            var instance = buffData.CreateBuffInstance(buffTarget);
            ModifyStat(buffTarget, instance.StatType, instance.ValueChange);
            return instance;
        }

        protected override void RemoveBuffInstance(IEntity buffTarget, StatBuffInstance statBuffInstance)
        {
            buffTarget.UnitActiveBuffsData.ActiveStatBuffs.Remove(statBuffInstance);
            ModifyStat(buffTarget, statBuffInstance.StatType, -statBuffInstance.ValueChange);
        }

        private void ModifyStat(IEntity unit, StatType statType, float delta)
        {
            _unitStatsModificationService.ModifyStat(unit, statType, delta);
        }
    }
}