using Gameplay.Combat.Data;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.HitBuffsCore;
using Gameplay.StatusEffects.Buffs.HitStreamBuffsCore;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public static class BuffApplicationService
    {
        private static readonly DefensiveHitBuffApplicationService _defensiveHitProcessor = new();
        private static readonly HitBuffApplicationService _hitBuffApplicationService = new();
        private static readonly HitStreamBuffApplicationService _hitStreamBuffApplicationService = new();

        public static void ApplyStructuralHitStreamBuffs(IGameUnit attacker, HitDataStream hitDataStream)
        {
            _hitStreamBuffApplicationService.ApplyStructuralHitStreamBuffs(attacker, hitDataStream);
        }

        public static void BuffHitStream(IGameUnit attacker, HitDataStream hitDataStream)
        {
            _hitStreamBuffApplicationService.ApplyHitStreamBuffs(attacker, hitDataStream);
        }

        public static void BuffOutgoingHits(in DamageContext damageContext)
        {
            damageContext.HitData.Damage = _hitBuffApplicationService.CalculateDamage(damageContext);
        }

        public static void DebuffIncomingHits(in DamageContext damageContext)
        {
            damageContext.HitData.Damage = _defensiveHitProcessor.CalculateDamage(damageContext);
        }
    }
}