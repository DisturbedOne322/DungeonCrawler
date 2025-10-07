using Gameplay.Combat.Modifiers;

namespace Gameplay.Buffs
{
    public struct DefensiveBuffInstance
    {
        public int TurnDurationLeft;
        public BuffExpirationType ExpirationType;
        public DefensiveBuffPriorityType PriorityType;
        public DefensiveBuffData DefensiveBuffData;

        public static DefensiveBuffInstance Create(DefensiveBuffData buffData)
        {
            return new DefensiveBuffInstance()
            {
                TurnDurationLeft = buffData.BuffDurationData.TurnDurations,
                ExpirationType = buffData.BuffDurationData.ExpirationType,
                PriorityType = buffData.Priority,
                DefensiveBuffData = buffData,
            };
        }
    }
}