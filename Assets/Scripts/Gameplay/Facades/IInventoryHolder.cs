using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.Units;

namespace Gameplay.Facades
{
    public interface IInventoryHolder
    {
        UnitInventoryData UnitInventoryData { get; }
        UnitEquipmentData UnitEquipmentData { get; }
    }
}