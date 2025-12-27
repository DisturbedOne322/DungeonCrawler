using Gameplay.Units;

namespace Gameplay.Facades
{
    public interface IStatsProvider
    {
        UnitStatsData UnitStatsData { get; }
        UnitBonusStatsData UnitBonusStatsData { get; }
    }
}