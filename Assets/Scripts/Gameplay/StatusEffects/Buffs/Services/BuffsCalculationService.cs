using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.OffensiveCore;

namespace Gameplay.StatusEffects.Buffs.Services
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