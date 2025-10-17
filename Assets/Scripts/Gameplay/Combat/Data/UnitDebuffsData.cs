using Gameplay.StatusEffects.Debuffs;
using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitDebuffsData
    {
        public readonly ReactiveCollection<StatDebuffData> StatDebuffs = new();
    }
}