using Gameplay.Facades;

namespace Gameplay.Combat.Data
{
    public struct SkillUsedData
    {
        public IGameUnit Attacker;
        public IGameUnit Target;
        public HitDataStream HitDataStream;
    }
}