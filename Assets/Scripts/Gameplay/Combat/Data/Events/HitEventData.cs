using Gameplay.Facades;

namespace Gameplay.Combat.Data.Events
{
    public struct HitEventData
    {
        public float HealthPercentBeforeHit;
        public ICombatant Attacker;
        public IGameUnit Target;
        public HitData HitData;
    }
}