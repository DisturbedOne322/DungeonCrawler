using Gameplay.Facades;

namespace Gameplay.Combat.Data.Events
{
    public struct EvadeEventData
    {
        public IGameUnit Attacker;
        public IGameUnit Target;
    }
}