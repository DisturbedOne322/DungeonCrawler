using Gameplay.Facades;

namespace Gameplay.Buffs.Core
{
    public interface IBuffProcessor
    {
        void ApplyPermanentBuffs(IEntity buffTarget);
        void EnableBuffsOnTrigger(IGameUnit unit, BuffTriggerType triggerType);
        void ProcessTurn(IEntity buffTarget);
        void RemoveBuffsOnAction(IGameUnit unit, BuffExpirationType expirationType);
        void ClearBuffs(IEntity buffTarget);
    }
}