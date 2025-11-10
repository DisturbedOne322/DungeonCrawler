using Gameplay.Facades;
using UniRx;

namespace Gameplay.StatusEffects.Core
{
    public abstract class BaseStatusEffectInstance
    {
        protected IEntity AffectedUnit;
        public StatusEffectExpirationType EffectExpirationType;
        public IntReactiveProperty Stacks = new(1);
        public BaseStatusEffectData StatusEffectData;
        public IntReactiveProperty TurnDurationLeft;

        public abstract void Apply(IEntity activeUnit, IEntity otherUnit);
        public abstract void Revert();
    }
}