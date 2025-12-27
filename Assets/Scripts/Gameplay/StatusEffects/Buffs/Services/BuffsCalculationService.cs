using Gameplay.Combat.Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using Gameplay.StatusEffects.Buffs.HitStreamBuffsCore;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class BuffsCalculationService
    {
        private readonly DefensiveHitBuffApplicationService _defensiveHitProcessor = new();
        private readonly HitBuffApplicationService _hitProcessor = new();
        private readonly HitStreamBuffApplicationService _hitStreamBuffApplicationService = new();

        public void ApplyStructuralHitStreamBuffs(IGameUnit attacker, HitDataStream hitDataStream)
        {
            _hitStreamBuffApplicationService.ApplyStructuralHitStreamBuffs(attacker, hitDataStream);
        }

        public void BuffHitStream(IGameUnit attacker, HitDataStream hitDataStream)
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