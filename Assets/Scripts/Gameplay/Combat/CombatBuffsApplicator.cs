using Gameplay.Facades;

namespace Gameplay.Combat
{
    public class CombatBuffsApplicator
    {
        public void ApplyGuardToUnit(IEntity unit) => unit.UnitBuffsData.Guarded.Value = true;
        public void ApplyChargeToUnit(IEntity unit) => unit.UnitBuffsData.Charged.Value = true;
        public void ApplyConcentrateToUnit(IEntity unit) => unit.UnitBuffsData.Concentrated.Value = true;
    }
}