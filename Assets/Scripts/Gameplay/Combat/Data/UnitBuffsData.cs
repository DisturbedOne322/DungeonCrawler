using Gameplay.Buffs;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;
using Gameplay.Buffs.StatBuffsCore;
using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitBuffsData
    {
        public readonly ReactiveCollection<OffensiveBuffData> OffensiveBuffs = new ();
        public readonly ReactiveCollection<DefensiveBuffData> DefensiveBuffs = new ();
        public readonly ReactiveCollection<StatBuffData> StatBuffs = new ();
    }
}