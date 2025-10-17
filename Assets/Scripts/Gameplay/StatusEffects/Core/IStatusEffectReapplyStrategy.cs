namespace Gameplay.StatusEffects.Core
{
    public interface IStatusEffectReapplyStrategy
    {
        public void ReapplyStatusEffect(BaseStatusEffectInstance instance, BaseStatusEffectData data);
    }
}