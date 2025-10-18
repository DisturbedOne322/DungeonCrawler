using System;
using System.Collections.Generic;
using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Core;
using Gameplay.StatusEffects.ReapplyStrategies;

namespace Helpers
{
    public static class StatusEffectsHelper
    {
        private static readonly Dictionary<DamageType, StatusEffectTriggerType> DamageTypeToTriggerDict = new()
        {
            { DamageType.Physical, StatusEffectTriggerType.PhysicalDamage },
            { DamageType.Magical, StatusEffectTriggerType.MagicalDamage },
            { DamageType.Absolute, StatusEffectTriggerType.AbsoluteDamage }
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
                { StatusEffectReapplyType.LowerDuration, new ReapplyLowerDurationStrategy() },
                { StatusEffectReapplyType.RefreshDuration, new ReapplyRefreshDurationStrategy() }
            };

        public static StatusEffectTriggerType GetBuffTriggerForDamageType(DamageType damageType)
        {
            if (DamageTypeToTriggerDict.TryGetValue(damageType, out var buffTrigger))
                return buffTrigger;

            throw new ArgumentException("Unknown damage type: " + damageType);
        }

        public static StatusEffectExpirationType GetExpirationForDamageType(DamageType damageType)
        {
            if (DamageTypeToExpirationDict.TryGetValue(damageType, out var expirationType))
                return expirationType;

            throw new ArgumentException("Unknown damage type: " + damageType);
        }

        public static void ReapplyStatusEffect(BaseStatusEffectInstance instance, BaseStatusEffectData data)
        {
            var type = data.StatusEffectReapplyType;

            foreach (var pair in BuffReapplyStrategies)
                if (type.HasFlag(pair.Key))
                    pair.Value.ReapplyStatusEffect(instance, data);
        }
        
        public static bool IsExpirationTypeActionBased(StatusEffectExpirationType effectExpirationType)
        {
            return effectExpirationType is
                StatusEffectExpirationType.NextAbsoluteAction or
                StatusEffectExpirationType.NextMagicalAction or
                StatusEffectExpirationType.NextPhysicalAction;
        }
    }
}