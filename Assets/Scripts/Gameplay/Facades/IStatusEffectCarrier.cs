using Gameplay.Units;

namespace Gameplay.Facades
{
    public interface IStatusEffectCarrier
    {
        UnitHeldStatusEffectsContainer UnitHeldStatusEffectsContainer { get; }
        UnitActiveStatusEffectsContainer UnitActiveStatusEffectsContainer { get; }
    }
}