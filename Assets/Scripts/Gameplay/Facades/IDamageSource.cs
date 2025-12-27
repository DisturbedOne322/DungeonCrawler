using Gameplay.Units;

namespace Gameplay.Facades
{
    public interface IDamageSource
    {
        UnitStatsData UnitStatsData { get; }
        UnitBonusStatsData UnitBonusStatsData { get; }
    }
}