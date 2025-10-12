using Constants;
using Gameplay.Buffs.Core;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;
using UnityEngine;

namespace Gameplay.Buffs.Services
{
    public class BuffsCalculationService
    {
        private readonly OffensiveBuffCalculationProcessor _offensiveProcessor = new();
        private readonly DefensiveBuffCalculationProcessor _defensiveProcessor = new();

        public void BuffOutgoingDamage(in DamageContext damageContext)
        {
            damageContext.HitData.Damage = _offensiveProcessor.CalculateDamage(damageContext);

            if (damageContext.HitData.IsCritical) 
                ApplyCriticalDamageMultiplier(damageContext);

            ApplyAttackMultiplier(damageContext);
        }

        public void DebuffIncomingDamage(in DamageContext damageContext)
        {
            damageContext.HitData.Damage = _defensiveProcessor.CalculateDamage(damageContext);
            ApplyDefenseMultiplier(damageContext);
        }

        private void ApplyCriticalDamageMultiplier(in DamageContext damageContext)
        {
            float damageMultiplier = GetCriticalDamageMultiplier(damageContext);
            int damage = damageContext.HitData.Damage;
            damageContext.HitData.Damage *= Mathf.RoundToInt(damage * damageMultiplier);
        }
        
        private void ApplyAttackMultiplier(in DamageContext damageContext)
        {
            float damageMultiplier = damageContext.Attacker.UnitBonusStatsData.AttackMultiplier.Value;
            int damage = damageContext.HitData.Damage;
            damageContext.HitData.Damage *= Mathf.RoundToInt(damage * damageMultiplier);
        }

        private void ApplyDefenseMultiplier(in DamageContext damageContext)
        {
            float defenseMultiplier = damageContext.Defender.UnitBonusStatsData.DefenseMultiplier.Value;
            int damage = damageContext.HitData.Damage;
            damageContext.HitData.Damage *= Mathf.RoundToInt(damage / defenseMultiplier);
        }
        
        private float GetCriticalDamageMultiplier(in DamageContext damageContext)
        {
            return GameplayConstants.CriticalDamageMultiplier +
                   damageContext.Attacker.UnitBonusStatsData.CritDamageBonus.Value;
        }
    }
}