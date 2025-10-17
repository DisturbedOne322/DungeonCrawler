using Data;
using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.StatBuffsCore
{
    public class StatBuffInstance : BaseStatusEffectInstance
    {
        public StatType StatType;
        public float ValueChange;

        public static StatBuffInstance Create(StatBuffData buffDataData, float valueChange)
        {
            return new StatBuffInstance
            {
                TurnDurationLeft = buffDataData.StatusEffectDurationData.TurnDurations,
                EffectExpirationType = buffDataData.StatusEffectDurationData.EffectExpirationType,
                StatType = buffDataData.BuffedStatType,
                StatusEffectData = buffDataData,
                ValueChange = valueChange
            };
        }
    }
}