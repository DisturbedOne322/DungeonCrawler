using Gameplay.Facades;
using Helpers;
using UniRx;

namespace Gameplay.StatusEffects.Core
{
    public abstract class BaseStatusEffectInstance
    {
        public StatusEffectExpirationType EffectExpirationType;
        public IntReactiveProperty Stacks = new(1);
        public BaseStatusEffectData StatusEffectData;
        public IntReactiveProperty DurationLeft;
        public StatusEffectContext Context;

        private bool _reverted = false;
        private bool _applied = false;
        
        public bool Applied => _applied;

        public void Apply(StatusEffectContext context)
        {
            if (_applied)
            {
                DebugHelper.LogWarning("Buff instance already applied");
                return;
            }
            
            Context = context;
            ProcessApply(context.ActiveUnit, context.OtherUnit);
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