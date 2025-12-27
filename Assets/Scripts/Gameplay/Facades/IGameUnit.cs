using Animations;
using Gameplay.Equipment;
using Gameplay.Units;
using Gameplay.Visual;

namespace Gameplay.Facades
{
    public interface IGameUnit : IEntity, ISkillUser, IDamageSource
    {
        EvadeAnimator EvadeAnimator { get; }
        AttackAnimator AttackAnimator { get; }
        
        UnitHeldStatusEffectsData UnitHeldStatusEffectsData { get; }
        UnitActiveStatusEffectsData UnitActiveStatusEffectsData { get; }
        
        UnitEquipmentData UnitEquipmentData { get; }
        UnitInventoryData UnitInventoryData { get; }
    }
}