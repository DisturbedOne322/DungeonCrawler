using Gameplay.Facades;

namespace Gameplay.Combat.Data
{
    public struct HitEventData
    {
        public IEntity Target;
        public int Damage;
        public HitDamageType HitDamageType;
    }
}