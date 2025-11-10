using System;
using Data;
using Gameplay.Facades;
using UniRx;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.StatBuffsCore
{
    public static class UnitStatsModificationService
    {
        public static float ModifyStat(IEntity unit, StatType statType, float delta)
        {
            if (TryModifyBaseStat(unit, statType, delta, out var applied)) return applied;
            if (TryModifyBonusStat(unit, statType, delta, out applied)) return applied;
            if (TryModifySpecialStat(unit, statType, delta, out applied)) return applied;

            throw new Exception($"{statType} is unhandled");
        }

        private static bool TryModifyBaseStat(IEntity unit, StatType type, float delta, out float applied)
        {
            applied = 0f;
            var stats = unit.UnitStatsData;
            var intDelta = Mathf.RoundToInt(delta);

            switch (type)
            {
                case StatType.Constitution:
                    applied = ApplyIntClamped(stats.Constitution, intDelta, 1);
                    return true;

                case StatType.Strength:
                    applied = ApplyIntClamped(stats.Strength, intDelta, 1);
                    return true;

                case StatType.Dexterity:
                    applied = ApplyIntClamped(stats.Dexterity, intDelta, 1);
                    return true;

                case StatType.Intelligence:
                    applied = ApplyIntClamped(stats.Intelligence, intDelta, 1);
                    return true;

                case StatType.Luck:
                    applied = ApplyIntClamped(stats.Luck, intDelta, 1);
                    return true;
            }

            return false;
        }

        private static bool TryModifyBonusStat(IEntity unit, StatType type, float delta, out float applied)
        {
            applied = 0f;
            var bonus = unit.UnitBonusStatsData;

            switch (type)
            {
                case StatType.CriticalChance:
                    applied = ApplyFloatClamped(bonus.CritChanceBonus, delta, 0f);
                    return true;

                case StatType.CriticalDamage:
                    applied = ApplyFloatClamped(bonus.CritDamageBonus, delta, 0f);
                    return true;

                case StatType.HealthRegen:
                    applied = ApplyIntClamped(bonus.HealthRegenBonus, Mathf.RoundToInt(delta), 0);
                    return true;

                case StatType.ManaRegen:
                    applied = ApplyIntClamped(bonus.ManaRegenBonus, Mathf.RoundToInt(delta), 0);
                    return true;

                case StatType.PenetrationRatio:
                    applied = ApplyFloatClamped(bonus.PenetrationRatio, delta, 0f);
                    return true;
            }

            return false;
        }

        private static bool TryModifySpecialStat(IEntity unit, StatType type, float delta, out float applied)
        {
            applied = 0f;
            var intDelta = Mathf.RoundToInt(delta);

            switch (type)
            {
                case StatType.Health:
                    applied = ModifyHealth(unit, intDelta);
                    return true;

                case StatType.Mana:
                    applied = ModifyMana(unit, intDelta);
                    return true;
            }

            return false;
        }

        private static float ModifyHealth(IEntity unit, int delta)
        {
            var healthData = unit.UnitHealthData;
            var healthController = unit.UnitHealthController;

            var oldMax = healthData.MaxHealth.Value;
            var requestedNewMax = oldMax + delta;
            var newMax = Mathf.Max(1, requestedNewMax);

            var appliedInt = newMax - oldMax;

            if (appliedInt != 0)
                healthController.IncreaseMaxHealth(appliedInt);

            return appliedInt;
        }

        private static float ModifyMana(IEntity unit, int delta)
        {
            var manaData = unit.UnitManaData;
            var manaController = unit.UnitManaController;

            var oldMax = manaData.MaxMana.Value;
            var requestedNewMax = oldMax + delta;
            var newMax = Mathf.Max(0, requestedNewMax);

            var appliedInt = newMax - oldMax;

            if (appliedInt != 0)
                manaController.IncreaseMaxMana(appliedInt);

            return appliedInt;
        }

        private static float ApplyIntClamped(ReactiveProperty<int> prop, int intDelta, int minValue)
        {
            var old = prop.Value;
            var requestedNew = old + intDelta;
            var newVal = Mathf.Max(minValue, requestedNew);
            prop.Value = newVal;
            return newVal - old;
        }

        private static float ApplyFloatClamped(ReactiveProperty<float> prop, float delta, float minValue)
        {
            var old = prop.Value;
            var newVal = Mathf.Max(minValue, old + delta);
            prop.Value = newVal;
            return newVal - old;
        }
    }
}