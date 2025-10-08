using Gameplay.Facades;

namespace Gameplay.Combat.Data
{
    public struct HitEventData
    {
        public IGameUnit Attacker;
        public IGameUnit Target;
        public int Damage;
        public bool IsCritical;
        public OffensiveSkillData SkillData;
    }
}