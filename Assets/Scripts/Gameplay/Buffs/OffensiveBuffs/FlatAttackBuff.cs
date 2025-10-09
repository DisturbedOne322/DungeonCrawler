using Gameplay.Buffs.Core;
using Gameplay.Buffs.OffensiveCore;
using UnityEngine;

namespace Gameplay.Buffs.OffensiveBuffs
{
    [CreateAssetMenu(fileName = "FlatAttackBuff", menuName = "Gameplay/Buffs/OffensiveBuffs/FlatAttackBuff")]
    public class FlatAttackBuff : OffensiveBuffData
    {
        [SerializeField] private int _damageIncrease;
        
        public override int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx)
        {
            return currentDamage + _damageIncrease;
        }
    }
}