using System;
using System.Collections.Generic;
using Data;
using Gameplay.Buffs.Core;
using Gameplay.Facades;
using UniRx;
using UnityEngine;

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
        
        private void ModifyStat(IEntity unit, StatType statType, float delta)
        {
            if (TryModifyBaseStat(unit, statType, delta)) return;
            if (TryModifyBonusStat(unit, statType, delta)) return;
            if (TryModifySpecialStat(unit, statType, delta)) return;

            throw new Exception($"{statType} is unhandled");
        }
        
        private bool TryModifyBaseStat(IEntity unit, StatType type, float delta)
        {
            var stats = unit.UnitStatsData;
            int intDelta = Mathf.RoundToInt(delta);

            switch (type)
            {
                case StatType.Constitution: stats.Constitution.Value += intDelta; return true;
                case StatType.Strength: stats.Strength.Value += intDelta; return true;
                case StatType.Dexterity: stats.Dexterity.Value += intDelta; return true;
                case StatType.Intelligence: stats.Intelligence.Value += intDelta; return true;
                case StatType.Luck: stats.Luck.Value += intDelta; return true;
            }
            return false;
        }

        private bool TryModifyBonusStat(IEntity unit, StatType type, float delta)
        {
            var bonus = unit.UnitBonusStatsData;

            switch (type)
            {
                case StatType.CriticalChance: bonus.CritChanceBonus.Value += delta; return true;
                case StatType.CriticalDamage: bonus.CritDamageBonus.Value += delta; return true;
                case StatType.AttackMultiplier: bonus.AttackMultiplier.Value += delta; return true;
                case StatType.DefenseMultiplier: bonus.DefenseMultiplier.Value += delta; return true;
                case StatType.HealthRegen: bonus.HealthRegenBonus.Value += Mathf.RoundToInt(delta); return true;
                case StatType.ManaRegen: bonus.ManaRegenBonus.Value += Mathf.RoundToInt(delta); return true;
                case StatType.PenetrationRatio: bonus.PenetrationRatio.Value += delta; return true;
            }
            return false;
        }

        private bool TryModifySpecialStat(IEntity unit, StatType type, float delta)
        {
            int intDelta = Mathf.RoundToInt(delta);

            switch (type)
            {
                case StatType.Health: BuffHealth(unit, intDelta); return true;
                case StatType.Mana: BuffMana(unit, intDelta); return true;
            }
            return false;
        }

        private void BuffHealth(IEntity unit, int delta)
        {
            var healthData = unit.UnitHealthData;
            var healthController = unit.UnitHealthController;
            var maxHealth = healthData.MaxHealth.Value;
            healthController.SetNewMaxHealth(maxHealth + delta);
        }
        
        private void BuffMana(IEntity unit, int delta)
        {
            var manaData = unit.UnitManaData;
            var manaController = unit.UnitManaController;
            var manMana = manaData.MaxMana.Value;
            manaController.SetNewMaxMana(manMana + delta);
        }
    }
}