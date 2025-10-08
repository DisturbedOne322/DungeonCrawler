using Gameplay.Combat.Data;
using UnityEngine;

namespace Helpers
{
    public static class DamageTypeToColorHelper
    {
        public static Color GetDamageTypeColor(HitEventData data)
        {
            var damageType = data.SkillData.DamageType;
            
            switch (damageType)
            {
                case DamageType.Physical:
                    return data.IsCritical ? Color.red : Color.white;
                case DamageType.Magical:
                    return data.IsCritical ? Color.cyan : Color.blue;
            }
            
            return Color.white;
        }
    }
}