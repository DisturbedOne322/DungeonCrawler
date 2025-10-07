using Gameplay.Buffs;
using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitBuffsData
    {
        public readonly ReactiveCollection<OffensiveBuffData> OffensiveBuffs = new ();
        public readonly ReactiveCollection<DefensiveBuffData> DefensiveBuffs = new ();
    }
}