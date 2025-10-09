using Gameplay.Combat.Data;
using Gameplay.Facades;

namespace Gameplay.Buffs.Core
{
    public readonly struct DamageContext
    {
        public IGameUnit Attacker { get; }
        public IGameUnit Defender { get; }
        public OffensiveSkillData SkillData { get; }
        public bool IsCritical { get; }
        public bool ConsumeCharge { get; }

        public DamageContext(IGameUnit attacker, IGameUnit defender, OffensiveSkillData skillData, bool isCritical, bool consumeCharge)
        {
            Attacker = attacker;
            Defender = defender;
            SkillData = skillData;
            IsCritical = isCritical;
            ConsumeCharge = consumeCharge;
        }
    }
}