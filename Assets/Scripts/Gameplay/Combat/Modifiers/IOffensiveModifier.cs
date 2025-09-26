namespace Gameplay.Combat.Modifiers
{
    public interface IOffensiveModifier
    {
        OffensiveModifierPriorityType Priority { get; }
        int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}