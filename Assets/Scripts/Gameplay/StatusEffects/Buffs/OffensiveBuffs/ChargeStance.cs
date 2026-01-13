using Data.Constants;
using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.OffensiveBuffs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayOffensiveBuffs + "ChargeStance")]
    public class ChargeStance : HitBuffData
    {
        [SerializeField] [Min(1)] private float _damageMultiplier;

        public override int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx)
        {
            if (ctx.HitData.DamageType is not DamageType.Physical)
                return currentDamage;

            return Mathf.RoundToInt(currentDamage * _damageMultiplier);
        }
    }
}