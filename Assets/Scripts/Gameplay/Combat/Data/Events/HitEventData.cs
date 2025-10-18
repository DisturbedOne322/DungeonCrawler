using Gameplay.Facades;

namespace Gameplay.Combat.Data.Events
{
    public struct HitEventData
    {
        public IGameUnit Attacker;
        public IGameUnit Target;
        public HitData HitData;
    }
}