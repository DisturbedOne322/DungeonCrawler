using Gameplay.Facades;

namespace Gameplay.Combat.Data
{
    public struct SkillCastedData
    {
        public IGameUnit Attacker;
        public IGameUnit Target;
        public HitDataStream HitDataStream;
    }
}