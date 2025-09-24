using Gameplay.Combat.Data;
using UnityEngine;

namespace Helpers
{
    public static class DamageTypeToColorHelper
    {
        public static Color GetDamageTypeColor(HitDamageType damageType)
        {
            switch (damageType)
            {
                case HitDamageType.Physical:
                    return Color.white;
                case HitDamageType.PhysicalCritical:
                    return Color.red;
                case HitDamageType.Magical:
                    return Color.blue;
                case HitDamageType.MagicalCritical:
                    return Color.cyan;
            }
            
            return Color.white;
        }
    }
}