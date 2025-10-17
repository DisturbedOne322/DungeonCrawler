using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.DefensiveCore
{
    public class DefensiveBuffInstance : BaseStatusEffectInstance
    {
        public DefensiveBuffData DefensiveBuffData;
        public DefensiveBuffPriorityType PriorityType;

        public static DefensiveBuffInstance Create(DefensiveBuffData buffData)
        {
            return new DefensiveBuffInstance
            {
                TurnDurationLeft = buffData.StatusEffectDurationData.TurnDurations,
                EffectExpirationType = buffData.StatusEffectDurationData.EffectExpirationType,
                PriorityType = buffData.Priority,
                StatusEffectData = buffData,
                DefensiveBuffData = buffData
            };
        }
    }
}