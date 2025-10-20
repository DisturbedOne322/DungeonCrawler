using Gameplay.Combat.Data;
using Gameplay.Combat.Data.Events;
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
                    return data.HitData.IsCritical.Value ? Color.red : Color.white;
                case DamageType.Magical:
                    return data.HitData.IsCritical.Value ? Color.cyan : Color.blue;
            }

            return Color.white;
        }

        public static Color GetHealColor()
        {
            return Color.green;
        }
    }
}