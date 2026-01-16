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
            if (!AppliesTo(ctx.HitData))
                return currentDamage;

            return Mathf.RoundToInt(currentDamage * _damageMultiplier);
        }

        public override float GetExpectedDamageMultiplier(HitData hitData)
        {
            if(!AppliesTo(hitData))
                return 1;

            return _damageMultiplier;
        }

        protected override bool AppliesTo(HitData hitData) => hitData.DamageType == DamageType.Physical;
    }
}