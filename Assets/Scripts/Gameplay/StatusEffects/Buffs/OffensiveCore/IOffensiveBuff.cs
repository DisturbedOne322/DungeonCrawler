namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public interface IOffensiveBuff
    {
        OffensiveBuffPriorityType Priority { get; }
        int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}