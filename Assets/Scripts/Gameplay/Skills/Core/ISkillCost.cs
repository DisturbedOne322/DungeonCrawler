using Gameplay.Combat.Services;

namespace Gameplay.Skills.Core
{
    public interface ISkillCost
    {
        bool CanPay(CombatService combatService);
        void Pay(CombatService combatService);
    }
}