using Data.Constants;
using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.OffensiveBuffs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayOffensiveBuffs + "FlatAttackBuff")]
    public class FlatAttackBuff : HitBuffData
    {
        [SerializeField] private int _damageIncrease;

        public override int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx)
        {
            return currentDamage + _damageIncrease;
        }

        public override float GetExpectedDamageMultiplier(HitData hitData)
        {
            return (hitData.Damage + _damageIncrease) * 1f / hitData.Damage;
        }

        protected override bool AppliesTo(HitData hitData) => true;
    }
}