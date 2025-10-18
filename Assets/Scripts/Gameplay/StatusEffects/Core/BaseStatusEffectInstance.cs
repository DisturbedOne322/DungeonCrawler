using Gameplay.Facades;

namespace Gameplay.StatusEffects.Core
{
    public abstract class BaseStatusEffectInstance
    {
        public StatusEffectExpirationType EffectExpirationType;
        public int Stacks = 1;
        public BaseStatusEffectData StatusEffectData;
        public int TurnDurationLeft;
        protected IEntity AffectedUnit;
        
        public abstract void Apply(IEntity activeUnit, IEntity otherUnit);
        public abstract void Revert();
    }
}