using Gameplay.Facades;

namespace Gameplay.Combat.Data.Events
{
    public struct HealEventData
    {
        public int Amount;
        public IGameUnit Target;
    }
}