namespace Gameplay.StatusEffects.Core
{
    public class HeldStatusEffectData
    {
        public BaseStatusEffectData Effect { get; }
        public BaseGameItem Source { get; }

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
    }
}