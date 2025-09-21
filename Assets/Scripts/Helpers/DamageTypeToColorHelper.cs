using Gameplay.Combat.Data;
using UnityEngine;

namespace Helpers
{
    public static class DamageTypeToColorHelper
    {
        public static Color GetDamageTypeColor(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Physical:
                    return Color.white;
                case DamageType.PhysicalCritical:
                    return Color.red;
                case DamageType.Magical:
                    return Color.blue;
                case DamageType.MagicalCritical:
                    return Color.cyan;
            }
            
            return Color.white;
        }
    }
}