using Gameplay.Combat.Modifiers;

namespace Gameplay.Buffs
{
    public class DefensiveBuffInstance : BaseBuffInstance
    {
        public DefensiveBuffPriorityType PriorityType;
        public DefensiveBuffData DefensiveBuffData;

        public static DefensiveBuffInstance Create(DefensiveBuffData buffData)
        {
            return new DefensiveBuffInstance()
            {
                TurnDurationLeft = buffData.BuffDurationData.TurnDurations,
                ExpirationType = buffData.BuffDurationData.ExpirationType,
                PriorityType = buffData.Priority,
                BuffData = buffData,
                DefensiveBuffData = buffData,
            };
        }
    }
}