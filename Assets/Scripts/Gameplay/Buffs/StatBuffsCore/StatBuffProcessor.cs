using System;
using System.Collections.Generic;
using Data;
using Gameplay.Buffs.Core;
using Gameplay.Facades;
using UniRx;

namespace Gameplay.Buffs.StatBuffsCore
{
    public class StatBuffProcessor : BaseBuffProcessor<StatBuffData, StatBuffInstance>
    {
        protected override ReactiveCollection<StatBuffData> GetBuffData(IEntity buffTarget) => buffTarget.UnitBuffsData.StatBuffs;

        protected override List<StatBuffInstance> GetActiveBuffs(IEntity buffTarget) => buffTarget.UnitActiveBuffsData.ActiveStatBuffs;

        protected override StatBuffInstance CreateBuffInstance(StatBuffData buffData, IEntity buffTarget)
        {
            var instance = buffData.CreateBuffInstance(buffTarget);
            ModifyStat(buffTarget, instance.StatType, instance.ValueChange);
            return instance;
        }

        protected override void RemoveBuffInstance(IEntity buffTarget, StatBuffInstance buffInstance)
        {
            buffTarget.UnitActiveBuffsData.ActiveStatBuffs.Remove(buffInstance);
            ModifyStat(buffTarget, buffInstance.StatType, -buffInstance.ValueChange);
        }
        
        private void ModifyStat(IEntity unit, StatType statType, int delta)
        {
            var stats = unit.UnitStatsData;

            switch (statType)
            {
                case StatType.Constitution:
                    stats.Constitution.Value += delta;
                    break;
                case StatType.Strength:
                    stats.Strength.Value += delta;
                    break;
                case StatType.Dexterity:
                    stats.Dexterity.Value += delta;
                    break;
                case StatType.Intelligence:
                    stats.Intelligence.Value += delta;
                    break;
                case StatType.Luck:
                    stats.Luck.Value += delta;
                    break;
                case StatType.Health:
                    BuffHealth(unit, delta);
                    break;
                case StatType.Mana:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void BuffHealth(IEntity unit, int delta)
        {
            var healthData = unit.UnitHealthData;
            var healthController = unit.UnitHealthController;
            var maxHealth = healthData.MaxHealth.Value;
            healthController.SetNewMaxHealth(maxHealth + delta);
        }
    }
}