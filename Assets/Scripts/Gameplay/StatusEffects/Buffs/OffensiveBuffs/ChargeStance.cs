using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.OffensiveBuffs
{
    [CreateAssetMenu(fileName = "ChargeStance", menuName = "Gameplay/Buffs/OffensiveBuffs/ChargeStance")]
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