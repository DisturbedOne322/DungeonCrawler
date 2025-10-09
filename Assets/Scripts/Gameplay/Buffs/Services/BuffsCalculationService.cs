using Gameplay.Buffs.Core;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;

namespace Gameplay.Buffs.Services
{
    public class BuffsCalculationService
    {
        private readonly OffensiveBuffCalculationProcessor _offensiveProcessor;
        private readonly DefensiveBuffCalculationProcessor _defensiveProcessor;

        public BuffsCalculationService()
        {
            _offensiveProcessor = new OffensiveBuffCalculationProcessor();
            _defensiveProcessor = new DefensiveBuffCalculationProcessor();
        }

        public int GetFinalOutgoingDamage(int baseDamage, in DamageContext damageContext)
        {
            var result = _offensiveProcessor.CalculateDamage(baseDamage, damageContext);

            if (damageContext.IsCritical)
                result *= 2;

            return result;
        }

        public int GetReducedIngoingDamage(int incomingDamage, in DamageContext damageContext)
        {
            return _defensiveProcessor.CalculateDamage(incomingDamage, damageContext);
        }
    }
}