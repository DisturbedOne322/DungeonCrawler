using Gameplay.Facades;

namespace Gameplay.Combat.Data
{
    public readonly struct DamageContext
    {
        public ICombatant Attacker { get; }
        public IGameUnit Defender { get; }
        public HitData HitData { get; }

        public DamageContext(ICombatant attacker, IGameUnit defender, HitData hitData)
        {
            Attacker = attacker;
            Defender = defender;
            HitData = hitData;
        }
    }
}