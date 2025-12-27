using Gameplay.Combat;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Facades
{
    public interface IEntity
    {
        string EntityName { get; }
        UnitHealthData UnitHealthData { get; }
        UnitManaData UnitManaData { get; }
        UnitHealthController UnitHealthController { get; }
        UnitManaController UnitManaController { get; }
        Vector3 Position { get; }
    }
}