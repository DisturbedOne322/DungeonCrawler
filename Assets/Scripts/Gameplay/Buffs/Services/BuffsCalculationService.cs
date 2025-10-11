using Gameplay.Buffs.Core;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;

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
                damageContext.HitData.Damage *= 2;
        }

        public void DebuffIncomingDamage(in DamageContext damageContext) => damageContext.HitData.Damage = _defensiveProcessor.CalculateDamage(damageContext);
    }
}