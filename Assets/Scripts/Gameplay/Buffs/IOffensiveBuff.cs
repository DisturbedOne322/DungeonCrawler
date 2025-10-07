namespace Gameplay.Combat.Modifiers
{
    public interface IOffensiveBuff
    {
        OffensiveBuffPriorityType Priority { get; }
        int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}