using System;
using System.Collections.Generic;
using Gameplay.Buffs.Core;
using Gameplay.Buffs.ReapplyStrategies;
using Gameplay.Combat.Data;

namespace Helpers
{
    public static class BuffsHelper
    {
        private static readonly Dictionary<DamageType, BuffTriggerType> DamageTypeToTriggerDict = new()
        {
            { DamageType.Physical, BuffTriggerType.PhysicalDamage },
            { DamageType.Magical, BuffTriggerType.MagicalDamage },
            { DamageType.Absolute, BuffTriggerType.AbsoluteDamage },
        };
        
        private static readonly Dictionary<DamageType, BuffExpirationType> DamageTypeToExpirationDict = new()
        {
            { DamageType.Physical, BuffExpirationType.NextPhysicalAction },
            { DamageType.Magical, BuffExpirationType.NextMagicalAction },
            { DamageType.Absolute, BuffExpirationType.NextAbsoluteAction },
        };

        private static readonly Dictionary<BuffReapplyType, IBuffReapplyStrategy> BuffReapplyStrategies = new()
        {
            { BuffReapplyType.None, new ReapplyIgnoreStrategy() },
            { BuffReapplyType.Stack, new ReapplyStackStrategy() },
            { BuffReapplyType.ExtendDuration, new ReapplyExtendDurationStrategy() },
            { BuffReapplyType.LowerDuration, new ReapplyLowerDurationStrategy() },
            { BuffReapplyType.RefreshDuration, new ReapplyRefreshDurationStrategy() },
        };

        public static BuffTriggerType GetBuffTriggerForDamageType(DamageType damageType)
        {
            if(DamageTypeToTriggerDict.TryGetValue(damageType, out BuffTriggerType buffTrigger))
                return buffTrigger;
            
            throw new ArgumentException("Unknown damage type: " + damageType);
        }

        public static BuffExpirationType GetExpirationForDamageType(DamageType damageType)
        {
            if(DamageTypeToExpirationDict.TryGetValue(damageType, out BuffExpirationType expirationType))
                return expirationType;
            
            throw new ArgumentException("Unknown damage type: " + damageType);
        }

        public static void ReapplyBuff(BaseBuffInstance instance, BaseBuffData data)
        {
            var type = data.BuffReapplyType;

            foreach (var pair in BuffReapplyStrategies)
            {
                if (type.HasFlag(pair.Key))
                    pair.Value.ReapplyBuff(instance, data);
            }
        }
    }
}