using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.OffensiveCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitBuffsData
    {
        public readonly ReactiveCollection<DefensiveBuffData> DefensiveBuffs = new();
        public readonly ReactiveCollection<OffensiveBuffData> OffensiveBuffs = new();
        public readonly ReactiveCollection<StatBuffData> StatBuffs = new();
    }
}