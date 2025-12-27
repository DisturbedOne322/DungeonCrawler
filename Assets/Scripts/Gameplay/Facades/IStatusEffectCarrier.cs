using Gameplay.Units;

namespace Gameplay.Facades
{
    public interface IStatusEffectCarrier
    {
        UnitHeldStatusEffectsData UnitHeldStatusEffectsData { get; }
        UnitActiveStatusEffectsData UnitActiveStatusEffectsData { get; }
    }
}