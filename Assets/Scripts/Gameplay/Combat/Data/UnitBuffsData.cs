using Gameplay.Combat.Modifiers;
using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitBuffsData
    {
        public readonly ReactiveProperty<bool> Guarded = new(false);
        public readonly ReactiveProperty<bool> Charged = new(false);
        public readonly ReactiveProperty<bool> Concentrated = new(false);

        public readonly ReactiveCollection<IOffensiveModifier> OffensiveBuffs = new ();
        public readonly ReactiveCollection<IDefensiveModifier> DefensiveBuffs = new ();
    }
}