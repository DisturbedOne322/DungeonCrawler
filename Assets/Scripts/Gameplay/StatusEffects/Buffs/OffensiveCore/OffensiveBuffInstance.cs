using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public class OffensiveBuffInstance : BaseStatusEffectInstance
    {
        public OffensiveStatusEffectData OffensiveStatusEffectData;
        public OffensiveBuffPriorityType PriorityType;

        public static OffensiveBuffInstance Create(OffensiveStatusEffectData statusEffectData)
        {
            return new OffensiveBuffInstance
            {
                TurnDurationLeft = statusEffectData.StatusEffectDurationData.TurnDurations,
                EffectExpirationType = statusEffectData.StatusEffectDurationData.EffectExpirationType,
                PriorityType = statusEffectData.Priority,
                StatusEffectData = statusEffectData,
                OffensiveStatusEffectData = statusEffectData
            };
        }
    }
}