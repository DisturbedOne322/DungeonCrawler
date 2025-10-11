using Gameplay.Buffs.Core;
using Gameplay.Buffs.OffensiveCore;
using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Buffs.OffensiveBuffs
{
    [CreateAssetMenu(fileName = "ChargeStance", menuName = "Gameplay/Buffs/OffensiveBuffs/ChargeStance")]
    public class ChargeStance : OffensiveBuffData
    {
        [SerializeField, Min(1)] private float _damageMultiplier;
    
        public override int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx)
        {
            if (ctx.HitData.DamageType is not DamageType.Physical)
                return currentDamage;
        
            return Mathf.RoundToInt(currentDamage * _damageMultiplier);
        }
    }
}
