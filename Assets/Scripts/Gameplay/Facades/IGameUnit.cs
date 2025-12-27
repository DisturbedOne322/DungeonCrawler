using Animations;
using Gameplay.Equipment;
using Gameplay.Units;
using Gameplay.Visual;

namespace Gameplay.Facades
{
    public interface IGameUnit : IEntity, ISkillUser, ICombatant
    {
        EvadeAnimator EvadeAnimator { get; }
        AttackAnimator AttackAnimator { get; }
        
        UnitEquipmentData UnitEquipmentData { get; }
        UnitInventoryData UnitInventoryData { get; }
    }
}