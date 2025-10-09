namespace Gameplay.Buffs.Core
{
    public interface IBuffApplicationProcessor
    {
        int CalculateDamage(int baseDamage, in DamageContext damageContext);
    }
}