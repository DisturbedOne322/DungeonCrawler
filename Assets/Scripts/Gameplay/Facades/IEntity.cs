using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using UnityEngine;

namespace Gameplay.Facades
{
    public interface IEntity
    {
        string EntityName { get; }
        UnitHealthData UnitHealthData { get; }
        UnitManaData UnitManaData { get; }
        UnitStatsData UnitStatsData { get; }
        UnitBonusStatsData UnitBonusStatsData { get; }
        UnitBuffsData UnitBuffsData { get; }
        UnitDebuffsData UnitDebuffsData { get; }
        UnitEquipmentData UnitEquipmentData { get; }
        UnitActiveBuffsData UnitActiveBuffsData { get; }
        UnitActiveDebuffsData UnitActiveDebuffsData { get; }
        UnitHealthController UnitHealthController { get; }
        UnitManaController UnitManaController { get; }
        Vector3 Position { get; }
    }
}