using Gameplay.Combat.Data;

namespace Gameplay.StatusEffects.Buffs.DefensiveCore
{
    public interface IDefensiveBuff
    {
        DefensiveBuffPriorityType Priority { get; }
        int ModifyIngoingDamage(int currentDamage, in DamageContext ctx);
    }
}