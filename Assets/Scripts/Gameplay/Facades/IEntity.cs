using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using UnityEngine;

namespace Gameplay.Facades
{
    public interface IEntity
    {
        string EntityName { get;}
        UnitHealthData UnitHealthData { get; }
        UnitStatsData UnitStatsData { get; }
        UnitBuffsData UnitBuffsData { get; }
        UnitEquipmentData UnitEquipmentData { get; }
        UnitActiveBuffsData UnitActiveBuffsData { get; }
        UnitHealthController UnitHealthController { get; }
        Vector3 Position { get; }
    }
}