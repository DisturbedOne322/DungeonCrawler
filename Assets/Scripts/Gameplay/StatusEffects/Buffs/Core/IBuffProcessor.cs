using Gameplay.Facades;
using Gameplay.StatusEffects.Core;

namespace Gameplay.StatusEffects.Buffs.Core
{
    public interface IBuffProcessor
    {
        void EnableBuffsOnTrigger(IGameUnit unit, StatusEffectTriggerType triggerType);
        void RemoveBuffsOnAction(IGameUnit unit, StatusEffectExpirationType effectExpirationType);
        void ClearBuffs(IEntity buffTarget);
        void ProcessTurn(IEntity buffTarget);
    }
}