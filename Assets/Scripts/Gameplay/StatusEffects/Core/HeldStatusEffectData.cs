namespace Gameplay.StatusEffects.Core
{
    public class HeldStatusEffectData
    {
        public HeldStatusEffectData(BaseStatusEffectData effect)
        {
            Effect = effect;
            Source = effect;
        }

        public HeldStatusEffectData(BaseStatusEffectData effect, BaseGameItem source)
        {
            Effect = effect;
            Source = source;
        }

        public BaseStatusEffectData Effect { get; }
        public BaseGameItem Source { get; }
    }
}