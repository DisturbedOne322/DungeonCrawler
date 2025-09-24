using Gameplay.Units;

namespace Gameplay.Combat.Data
{
    public struct HitEventData
    {
        public GameUnit Target;
        public int Damage;
        public DamageType DamageType;
    }
}