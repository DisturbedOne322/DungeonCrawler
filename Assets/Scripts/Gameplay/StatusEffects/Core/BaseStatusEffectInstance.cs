using Gameplay.Facades;
using Helpers;
using UniRx;

namespace Gameplay.StatusEffects.Core
{
    public abstract class BaseStatusEffectInstance
    {
        protected ICombatant AffectedUnit;
        public StatusEffectExpirationType EffectExpirationType;
        public IntReactiveProperty Stacks = new(1);
        public BaseStatusEffectData StatusEffectData;
        public IntReactiveProperty DurationLeft;

        private bool _reverted = false;
        private bool _applied = false;

        public void Apply(ICombatant activeUnit, ICombatant otherUnit)
        {
            if (_applied)
            {
                DebugHelper.LogWarning("Buff instance already applied");
                return;
            }
            
            ProcessApply(activeUnit, otherUnit);
            _applied = true;
        }

        public void Revert()
        {
            if (_reverted)
            {
                DebugHelper.LogWarning("Buff instance already reverted");
                return;
            }
            
            ProcessRevert();
            _reverted = true;
        }

        protected abstract void ProcessApply(ICombatant activeUnit, ICombatant otherUnit);

        protected abstract void ProcessRevert();
    }
}