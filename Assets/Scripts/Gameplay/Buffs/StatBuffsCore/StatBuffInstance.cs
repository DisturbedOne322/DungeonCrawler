using Data;
using Gameplay.Buffs.Core;

namespace Gameplay.Buffs.StatBuffsCore
{
    public class StatBuffInstance : BaseBuffInstance
    {
        public StatType StatType;
        public float ValueChange;
        public StatBuffData StatBuffData;

        public static StatBuffInstance Create(StatBuffData buffDataData, float valueChange)
        {
            return new StatBuffInstance()
            {
                TurnDurationLeft = buffDataData.BuffDurationData.TurnDurations,
                ExpirationType = buffDataData.BuffDurationData.ExpirationType,
                StatType = buffDataData.BuffedStatType,
                BuffData = buffDataData,
                StatBuffData = buffDataData,
                ValueChange = valueChange
            };
        }
    }
}