using Data;
using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Debuffs
{
    public class StatDebuffInstance : BaseStatusEffectInstance
    {
        public StatDebuffData StatDebuffData;
        public StatType StatType;
        public float ValueChange;

        public static StatDebuffInstance Create(StatDebuffData buffDataData, float valueChange)
        {
            return new StatDebuffInstance
            {
                TurnDurationLeft = buffDataData.StatusEffectDurationData.TurnDurations,
                EffectExpirationType = buffDataData.StatusEffectDurationData.EffectExpirationType,
                StatType = buffDataData.DebuffedStatType,
                StatusEffectData = buffDataData,
                StatDebuffData = buffDataData,
                ValueChange = valueChange
            };
        }
    }
}