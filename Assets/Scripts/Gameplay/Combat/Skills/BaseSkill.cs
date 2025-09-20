namespace Gameplay.Combat.Skills
{
    public class BaseSkill
    {
        public void DealDamage(int amount, GameUnit target) => target.UnitHealthController.TakeDamage(amount);
    }
}