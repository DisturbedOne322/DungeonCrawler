using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.OffensiveBuffs
{
    [CreateAssetMenu(fileName = "FlatAttackBuff", menuName = "Gameplay/Buffs/OffensiveBuffs/FlatAttackBuff")]
    public class FlatAttackBuff : HitBuffData
    {
        [SerializeField] private int _damageIncrease;

        public override int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx)
        {
            return currentDamage + _damageIncrease;
        }
    }
}