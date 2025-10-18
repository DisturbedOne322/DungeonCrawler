namespace Gameplay.StatusEffects.Buffs.OffensiveCore
{
    public interface IHitBuff
    {
        HitBuffPriorityType Priority { get; }
        int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}