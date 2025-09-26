namespace Gameplay.Combat.Modifiers
{
    public interface IDefensiveModifier
    {
        DefensiveModifierPriorityType Priority { get; }
        int ModifyIngoingDamage(int currentDamage, in DamageContext ctx);
    }
}