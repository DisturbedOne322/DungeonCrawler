using Gameplay.Buffs.Core;

namespace Gameplay.Buffs.OffensiveCore
{
    public interface IOffensiveBuff
    {
        OffensiveBuffPriorityType Priority { get; }
        int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}