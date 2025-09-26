using Gameplay.Combat.Data;
using Gameplay.Equipment;

namespace Gameplay.Facades
{
    public interface IInventoryHolder
    {
        UnitInventoryData UnitInventoryData { get; }
        UnitEquipmentData UnitEquipmentData { get; }
    }
}