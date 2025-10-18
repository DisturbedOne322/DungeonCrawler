using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public class OffensiveBuffInstance : BaseStatusEffectInstance
    {
        public OffensiveBuffData OffensiveBuffData;
        public OffensiveBuffPriorityType PriorityType;

        public static OffensiveBuffInstance Create(OffensiveBuffData buffData)
        {
            return new OffensiveBuffInstance
            {
                TurnDurationLeft = buffData.StatusEffectDurationData.TurnDurations,
                EffectExpirationType = buffData.StatusEffectDurationData.EffectExpirationType,
                PriorityType = buffData.Priority,
                StatusEffectData = buffData,
                OffensiveBuffData = buffData
            };
        }
    }
}