using Gameplay.Combat.SkillSelection;
using Gameplay.Units;

namespace Gameplay.Facades
{
    public interface ISkillUser
    {
        UnitSkillsData UnitSkillsData { get; }
        ActionSelectionProvider ActionSelectionProvider { get; }
    }
}