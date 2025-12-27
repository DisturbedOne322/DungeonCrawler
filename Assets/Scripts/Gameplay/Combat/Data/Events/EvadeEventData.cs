using Gameplay.Facades;

namespace Gameplay.Combat.Data.Events
{
    public struct EvadeEventData
    {
        public ICombatant Attacker;
        public IGameUnit Target;
    }
}