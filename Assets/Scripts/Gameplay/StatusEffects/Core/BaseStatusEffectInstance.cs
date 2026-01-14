using Gameplay.Facades;
using Helpers;
using UniRx;

namespace Gameplay.StatusEffects.Core
{
    public abstract class BaseStatusEffectInstance
    {
        private bool _reverted;
        public StatusEffectContext Context;
        public IntReactiveProperty DurationLeft;
        public StatusEffectExpirationType EffectExpirationType;
        public IntReactiveProperty Stacks = new(1);
        public BaseStatusEffectData StatusEffectData;

        public bool Applied { get; private set; }

        public void Apply(StatusEffectContext context)
        {
            if (Applied)
            {
                DebugHelper.LogWarning("Buff instance already applied");
                return;
            }

            Context = context;
            ProcessApply(context.ActiveUnit, context.OtherUnit);
            Applied = true;
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