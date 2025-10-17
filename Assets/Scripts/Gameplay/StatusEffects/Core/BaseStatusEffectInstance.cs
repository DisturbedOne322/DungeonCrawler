namespace Gameplay.StatusEffects.Core
{
    public abstract class BaseStatusEffectInstance
    {
        public StatusEffectExpirationType EffectExpirationType;
        public int Stacks = 1;
        public BaseStatusEffectData StatusEffectData;
        public int TurnDurationLeft;
    }
}