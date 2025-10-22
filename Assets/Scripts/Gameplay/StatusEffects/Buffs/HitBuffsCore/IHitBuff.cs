namespace Gameplay.StatusEffects.Buffs.HitBuffsCore
{
    public interface IHitBuff
    {
        HitBuffPriorityType Priority { get; }
        int ModifyOutgoingDamage(int currentDamage, in DamageContext ctx);
    }
}