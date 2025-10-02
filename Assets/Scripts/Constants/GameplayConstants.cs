using UnityEngine;

namespace Constants
{
    public class GameplayConstants
    {
        public static readonly Color HealColor = Color.green;
        
        public static readonly Vector2 RoomSize = new (10f, 10f);
        
        public const float DelayAfterAction = 0.5f;
        public const float DelayBeforeAction = 0.15f;
    }
}