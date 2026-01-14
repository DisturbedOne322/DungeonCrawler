using System;
using System.Collections.Generic;
using Gameplay;
using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Core;
using Gameplay.StatusEffects.ReapplyStrategies;

namespace Helpers
{
    public static class StatusEffectsHelper
    {
        private static readonly Dictionary<DamageType, StatusEffectTriggerType> DamageTypeToTriggerDoneDict = new()
        {
            { DamageType.Physical, StatusEffectTriggerType.OnPhysicalDamageDealt },
            { DamageType.Magical, StatusEffectTriggerType.OnMagicalDamageDealt },
            { DamageType.Absolute, StatusEffectTriggerType.OnAbsoluteDamageDealt }
        };

        private static readonly Dictionary<DamageType, StatusEffectTriggerType> DamageTypeToTriggerTakenDict = new()
        {
            { DamageType.Physical, StatusEffectTriggerType.OnPhysicalDamageTaken },
            { DamageType.Magical, StatusEffectTriggerType.OnMagicalDamageTaken },
            { DamageType.Absolute, StatusEffectTriggerType.OnAbsoluteDamageTaken }
        };

        private static readonly Dictionary<DamageType, StatusEffectExpirationType> DamageTypeToExpirationDict = new()
        {
            { DamageType.Physical, StatusEffectExpirationType.NextPhysicalAction },
            { DamageType.Magical, StatusEffectExpirationType.NextMagicalAction },
            { DamageType.Absolute, StatusEffectExpirationType.NextAbsoluteAction }
        };

        private static readonly Dictionary<StatusEffectReapplyType, IStatusEffectReapplyStrategy>
            BuffReapplyStrategies = new()
            {
                { StatusEffectReapplyType.None, new ReapplyIgnoreStrategy() },
                { StatusEffectReapplyType.Stack, new ReapplyStackStrategy() },
                { StatusEffectReapplyType.ExtendDuration, new ReapplyExtendDurationStrategy() },
                { StatusEffectReapplyType.RefreshDuration, new ReapplyRefreshDurationStrategy() }
            };

        public static StatusEffectTriggerType GetBuffTriggerForDamageTypeDealt(DamageType damageType)
        {
            if (DamageTypeToTriggerDoneDict.TryGetValue(damageType, out var buffTrigger))
                return buffTrigger;

            throw new ArgumentException("Unknown damage type: " + damageType);
        }

        public static StatusEffectTriggerType GetBuffTriggerForDamageTypeTaken(DamageType damageType)
        {
            if (DamageTypeToTriggerTakenDict.TryGetValue(damageType, out var buffTrigger))
                return buffTrigger;

            throw new ArgumentException("Unknown damage type: " + damageType);
        }

        public static StatusEffectExpirationType GetExpirationForDamageType(DamageType damageType)
        {
            if (DamageTypeToExpirationDict.TryGetValue(damageType, out var expirationType))
                return expirationType;

            throw new ArgumentException("Unknown damage type: " + damageType);
        }

        public static void ReapplyStatusEffect(BaseStatusEffectInstance instance)
        {
            var type = instance.StatusEffectData.StatusEffectReapplyType;

            foreach (var pair in BuffReapplyStrategies)
                if (type.HasFlag(pair.Key))
                    pair.Value.ReapplyStatusEffect(instance);
        }

        public static bool IsExpirationTypeActionBased(StatusEffectExpirationType effectExpirationType)
        {
            return effectExpirationType is
                StatusEffectExpirationType.NextAbsoluteAction or
                StatusEffectExpirationType.NextMagicalAction or
                StatusEffectExpirationType.NextPhysicalAction;
        }

        public static bool SameSource(BaseStatusEffectInstance instance, BaseGameItem source)
        {
            var context = instance.Context;
            return context.Source == source;
        }

        public static bool IsTriggeredOnObtain(BaseStatusEffectData data)
        {
            return data.TriggerType == StatusEffectTriggerType.OnObtained;
        }

        public static bool IsClearableStatusEffect(BaseStatusEffectInstance instance)
        {
            return GetExpirationType(instance) is not (StatusEffectExpirationType.Permanent
                or StatusEffectExpirationType.DepthIncrease);
        }

        public static bool IsTurnBased(BaseStatusEffectInstance instance)
        {
            return GetExpirationType(instance) is StatusEffectExpirationType.TurnCount;
        }

        public static bool IsDepthBased(BaseStatusEffectInstance instance)
        {
            return GetExpirationType(instance) is StatusEffectExpirationType.DepthIncrease;
        }

        private static StatusEffectExpirationType GetExpirationType(BaseStatusEffectInstance instance)
        {
            return instance.EffectExpirationType;
        }
    }
}