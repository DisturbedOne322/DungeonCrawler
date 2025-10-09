using Data;
using Gameplay.Buffs.Core;

namespace Gameplay.Buffs.StatBuffsCore
{
    public class StatBuffInstance : BaseBuffInstance
    {
        public StatBuffPriorityType PriorityType;
        public StatType StatType;
        public int ValueChange;
        public StatBuffData StatBuffData;

        public static StatBuffInstance Create(StatBuffData buffDataData, int valueChange)
        {
            return new StatBuffInstance()
            {
                TurnDurationLeft = buffDataData.BuffDurationData.TurnDurations,
                ExpirationType = buffDataData.BuffDurationData.ExpirationType,
                PriorityType = buffDataData.PriorityType,
                StatType = buffDataData.BuffedStatType,
                BuffData = buffDataData,
                StatBuffData = buffDataData,
                ValueChange = valueChange
            };
        }
    }
}