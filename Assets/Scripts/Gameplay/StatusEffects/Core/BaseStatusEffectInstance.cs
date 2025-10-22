using Gameplay.Facades;
using UniRx;

namespace Gameplay.StatusEffects.Core
{
    public abstract class BaseStatusEffectInstance
    {
        public StatusEffectExpirationType EffectExpirationType;
        public IntReactiveProperty Stacks = new(1);
        public BaseStatusEffectData StatusEffectData;
        public IntReactiveProperty TurnDurationLeft;
        protected IEntity AffectedUnit;
        
        public abstract void Apply(IEntity activeUnit, IEntity otherUnit);
        public abstract void Revert();
    }
}