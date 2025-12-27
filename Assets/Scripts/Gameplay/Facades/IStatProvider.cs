using Gameplay.Units;

namespace Gameplay.Facades
{
    public interface IStatProvider
    {
        UnitStatsData UnitStatsData { get; }
        UnitBonusStatsData UnitBonusStatsData { get; }
    }
}