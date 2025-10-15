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
        }

        public void DebuffIncomingDamage(in DamageContext damageContext)
        {
            damageContext.HitData.Damage = _defensiveProcessor.CalculateDamage(damageContext);
        }
    }
}