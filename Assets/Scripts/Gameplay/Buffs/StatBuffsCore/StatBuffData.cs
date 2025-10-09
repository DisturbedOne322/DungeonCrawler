using System;
using Data;
using Gameplay.Buffs.Core;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.Buffs.StatBuffsCore
{
    public abstract class StatBuffData : BaseBuffData
    {
        [SerializeField] private StatType _buffedStatType;
        [SerializeField] private StatBuffPriorityType _priorityType;

        public StatType BuffedStatType => _buffedStatType;
        public StatBuffPriorityType PriorityType => _priorityType;

        public StatBuffInstance ApplyTo(IEntity unit)
        {
            var buffDelta = GetBuffDelta(unit);
            
            var instance = StatBuffInstance.Create(this, buffDelta);
            unit.UnitActiveBuffsData.ActiveStatBuffs.Add(instance);

            ModifyStat(unit, buffDelta);
            return instance;
        }

        public void RemoveFrom(IEntity unit, StatBuffInstance instance)
        {
            ModifyStat(unit, -instance.ValueChange);
        }
        
        protected abstract int GetBuffDelta(IEntity unit);

        private void ModifyStat(IEntity unit, int delta)
        {
            var stats = unit.UnitStatsData;

            switch (_buffedStatType)
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
                    var healthData = unit.UnitHealthData;
                    var healthController = unit.UnitHealthController;
                    var maxHealth = healthData.MaxHealth.Value;
                    healthController.SetNewMaxHealth(maxHealth + delta);
                    break;
                case StatType.Mana:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}