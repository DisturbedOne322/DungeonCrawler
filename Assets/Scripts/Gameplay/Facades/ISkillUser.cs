using Gameplay.Combat.SkillSelection;
using Gameplay.Units;

namespace Gameplay.Facades
{
    public interface ISkillUser
    {
        UnitSkillsContainer UnitSkillsContainer { get; }
        BaseActionSelectionProvider BaseActionSelectionProvider { get; }
    }
}