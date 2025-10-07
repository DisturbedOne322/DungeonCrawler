using Gameplay.Buffs;
using Gameplay.Combat.Data;
using Gameplay.Combat.Modifiers;
using UnityEngine;

[CreateAssetMenu(fileName = "ChargeStance", menuName = "Gameplay/Buffs/OffensiveBuffs/ChargeStance")]
public class ChargeStance : OffensiveBuffData
{
    [SerializeField, Min(1)] private float _damageMultiplier;
    
    public override int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx)
    {
        if (ctx.SkillData.DamageType is not DamageType.Physical)
            return currentDamage;
        
        return Mathf.RoundToInt(currentDamage * _damageMultiplier);
    }
}
