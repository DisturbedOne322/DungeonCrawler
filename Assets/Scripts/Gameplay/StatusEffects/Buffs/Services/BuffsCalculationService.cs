using Gameplay.Combat.Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitStreamBuffsCore;
using Gameplay.StatusEffects.Buffs.OffensiveCore;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class BuffsCalculationService
    {
        private readonly HitStreamBuffApplicationService _hitStreamBuffApplicationService = new();
        private readonly HitBuffApplicationService _hitProcessor = new();
        private readonly DefensiveHitBuffApplicationService _defensiveHitProcessor = new();

        public void BuffHitStream(IEntity attacker, HitDataStream hitDataStream)
        {
            _hitStreamBuffApplicationService.ApplyHitStreamBuffs(attacker, hitDataStream);
        }
        
        public void BuffOutgoingDamage(in DamageContext damageContext)
        {
            damageContext.HitData.Damage = _hitProcessor.CalculateDamage(damageContext);
        }

        public void DebuffIncomingDamage(in DamageContext damageContext)
        {
            damageContext.HitData.Damage = _defensiveHitProcessor.CalculateDamage(damageContext);
        }
    }
}