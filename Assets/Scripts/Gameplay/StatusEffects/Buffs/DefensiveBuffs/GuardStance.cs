using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.DefensiveBuffs
{
    [CreateAssetMenu(fileName = "GuardStance", menuName = "Gameplay/Buffs/DefensiveBuffs/GuardStance")]
    public class GuardStance : DefensiveBuffData
    {
        [SerializeField] [Range(0, 1)] private float _damageMultiplier;

        public override int ModifyIngoingDamage(int currentDamage, in DamageContext ctx)
        {
            return Mathf.RoundToInt(currentDamage * _damageMultiplier);
        }
    }
}