using Gameplay.Buffs.Core;

namespace Gameplay.Buffs.DefensiveCore
{
    public interface IDefensiveBuff
    {
        DefensiveBuffPriorityType Priority { get; }
        int ModifyIngoingDamage(int currentDamage, in DamageContext ctx);
    }
}