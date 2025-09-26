using Gameplay.Combat;
using Gameplay.Combat.Data;
using UnityEngine;

namespace Gameplay.Facades
{
    public interface IEntity : IModifiersProvider
    {
        UnitHealthData UnitHealthData { get; }
        UnitStatsData UnitStatsData { get; }
        UnitBuffsData UnitBuffsData { get; }
        UnitHealthController UnitHealthController { get; }
        Vector3 Position { get; }
    }
}