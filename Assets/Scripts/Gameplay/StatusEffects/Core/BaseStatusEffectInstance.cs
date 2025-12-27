using Gameplay.Facades;
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

        public abstract void Apply(ICombatant activeUnit, ICombatant otherUnit);
        public abstract void Revert();
    }
}