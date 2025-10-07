using Gameplay.Combat.Modifiers;

namespace Gameplay.Buffs
{
    public struct OffensiveBuffInstance
    {
        public int TurnDurationLeft;
        public BuffExpirationType ExpirationType;
        public OffensiveBuffPriorityType PriorityType;
        public OffensiveBuffData OffensiveBuffData;
        
        public static OffensiveBuffInstance Create(OffensiveBuffData buffData)
        {
            return new OffensiveBuffInstance()
            {
                TurnDurationLeft = buffData.BuffDurationData.TurnDurations,
                ExpirationType = buffData.BuffDurationData.ExpirationType,
                PriorityType = buffData.Priority,
                OffensiveBuffData = buffData,
            };
        }
    }
}