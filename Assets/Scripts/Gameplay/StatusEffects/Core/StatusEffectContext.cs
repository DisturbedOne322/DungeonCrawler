using Gameplay.Facades;

namespace Gameplay.StatusEffects.Core
{
    public class StatusEffectContext
    {
        public readonly ICombatant ActiveUnit;
        public readonly ICombatant OtherUnit;
        public readonly BaseGameItem Source;

        public StatusEffectContext(BaseGameItem source, ICombatant activeUnit, ICombatant otherUnit)
        {
            Source = source;
            ActiveUnit = activeUnit;
            OtherUnit = otherUnit;
        }

        public ICombatant AffectedUnit { get; private set; }

        public void SetAffectedUnit(ICombatant affectedUnit)
        {
            AffectedUnit = affectedUnit;
        }
    }
}