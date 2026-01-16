using Data.Constants;
using Gameplay.Combat.Data;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using UnityEngine;

namespace Gameplay.StatusEffects.Buffs.DefensiveBuffs
{
    [CreateAssetMenu(menuName = MenuPaths.GameplayDefensiveBuffs + "GuardStance")]
    public class GuardStance : DefensiveBuffData
    {
        [SerializeField] [Range(0, 1)] private float _damageMultiplier;

        public override int ModifyIngoingDamage(int currentDamage, in DamageContext ctx)
        {
            return Mathf.RoundToInt(currentDamage * _damageMultiplier);
        }

        public override float GetDamageReductionMultiplier(HitBuffInstance hitBuff)
        {
            return _damageMultiplier;
        }
    }
}