using System;
using Data;
using Gameplay.Facades;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.StatBuffsCore
{
    public class UnitStatsModificationService
    {
        public void ModifyStat(IEntity unit, StatType statType, float delta)
        {
            if (TryModifyBaseStat(unit, statType, delta)) return;
            if (TryModifyBonusStat(unit, statType, delta)) return;
            if (TryModifySpecialStat(unit, statType, delta)) return;

            throw new Exception($"{statType} is unhandled");
        }

        private bool TryModifyBaseStat(IEntity unit, StatType type, float delta)
        {
            var stats = unit.UnitStatsData;
            var intDelta = Mathf.RoundToInt(delta);

            switch (type)
            {
                case StatType.Constitution:
                    stats.Constitution.Value += intDelta;
                    return true;
                case StatType.Strength:
                    stats.Strength.Value += intDelta;
                    return true;
                case StatType.Dexterity:
                    stats.Dexterity.Value += intDelta;
                    return true;
                case StatType.Intelligence:
                    stats.Intelligence.Value += intDelta;
                    return true;
                case StatType.Luck:
                    stats.Luck.Value += intDelta;
                    return true;
            }

            return false;
        }

        private bool TryModifyBonusStat(IEntity unit, StatType type, float delta)
        {
            var bonus = unit.UnitBonusStatsData;

            switch (type)
            {
                case StatType.CriticalChance:
                    bonus.CritChanceBonus.Value += delta;
                    return true;
                case StatType.CriticalDamage:
                    bonus.CritDamageBonus.Value += delta;
                    return true;
                case StatType.AttackMultiplier:
                    bonus.AttackMultiplier.Value += delta;
                    return true;
                case StatType.DefenseMultiplier:
                    bonus.DefenseMultiplier.Value += delta;
                    return true;
                case StatType.HealthRegen:
                    bonus.HealthRegenBonus.Value += Mathf.RoundToInt(delta);
                    return true;
                case StatType.ManaRegen:
                    bonus.ManaRegenBonus.Value += Mathf.RoundToInt(delta);
                    return true;
                case StatType.PenetrationRatio:
                    bonus.PenetrationRatio.Value += delta;
                    return true;
            }

            return false;
        }

        private bool TryModifySpecialStat(IEntity unit, StatType type, float delta)
        {
            var intDelta = Mathf.RoundToInt(delta);

            switch (type)
            {
                case StatType.Health:
                    ModifyHealth(unit, intDelta);
                    return true;
                case StatType.Mana:
                    ModifyMana(unit, intDelta);
                    return true;
            }

            return false;
        }

        private void ModifyHealth(IEntity unit, int delta)
        {
            var healthData = unit.UnitHealthData;
            var healthController = unit.UnitHealthController;
            var maxHealth = healthData.MaxHealth.Value;
            healthController.SetNewMaxHealth(maxHealth + delta);
        }

        private void ModifyMana(IEntity unit, int delta)
        {
            var manaData = unit.UnitManaData;
            var manaController = unit.UnitManaController;
            var manMana = manaData.MaxMana.Value;
            manaController.SetNewMaxMana(manMana + delta);
        }
    }
}