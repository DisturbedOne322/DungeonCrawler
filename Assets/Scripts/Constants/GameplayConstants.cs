using UnityEngine;

namespace Constants
{
    public class GameplayConstants
    {
        public const float DelayAfterAction = 0.5f;
        public const float DelayBeforeAction = 0.15f;

        public const float CriticalDamageMultiplier = 2f;

        public const float RegenerationRateOutOfCombat = 0.2f;

        public const int MaxBalance = 999_999_999;
        public static readonly Vector2 RoomSize = new(10f, 10f);
    }
}