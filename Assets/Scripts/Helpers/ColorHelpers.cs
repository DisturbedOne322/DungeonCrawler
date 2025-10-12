using Gameplay.Combat.Data;
using UnityEngine;

namespace Helpers
{
    public static class ColorHelpers
    {
        public static Color GetDamageTypeColor(HitEventData data)
        {
            var damageType = data.HitData.DamageType;
            
            switch (damageType)
            {
                case DamageType.Physical:
                    return data.HitData.IsCritical ? Color.red : Color.white;
                case DamageType.Magical:
                    return data.HitData.IsCritical ? Color.cyan : Color.blue;
            }
            
            return Color.white;
        }
        
        public static Color GetHealColor() => Color.green;
    }
}