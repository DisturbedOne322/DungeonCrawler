namespace Gameplay.StatusEffects.Buffs.Core
{
    public interface IBuffApplicationProcessor
    {
        int CalculateDamage(int baseDamage, in DamageContext damageContext);
    }
}