using Gameplay.Combat.Data;
using UnityEngine;

namespace Helpers
{
    public static class DamageTypeToColorHelper
    {
        public static Color GetDamageTypeColor(HitEventData data)
        {
            var damageType = data.HitDamageType;
            
            switch (damageType)
            {
                case HitDamageType.Physical:
                    return data.IsCritical ? Color.red : Color.white;
                case HitDamageType.Magical:
                    return data.IsCritical ? Color.cyan : Color.blue;
            }
            
            return Color.white;
        }
    }
}