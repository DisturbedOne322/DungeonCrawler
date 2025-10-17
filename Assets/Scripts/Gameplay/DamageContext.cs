using Gameplay.Combat.Data;
using Gameplay.Facades;

namespace Gameplay
{
    public readonly struct DamageContext
    {
        public IGameUnit Attacker { get; }
        public IGameUnit Defender { get; }
        public HitData HitData { get; }
        public SkillData SkillData { get; }

        public DamageContext(IGameUnit attacker, IGameUnit defender, HitData hitData, SkillData skillData)
        {
            Attacker = attacker;
            Defender = defender;
            HitData = hitData;
            SkillData = skillData;
        }
    }
}