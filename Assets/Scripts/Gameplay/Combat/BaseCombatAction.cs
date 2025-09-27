using Cysharp.Threading.Tasks;
using Data;

namespace Gameplay.Combat
{
    public abstract class BaseCombatAction : BaseGameItem
    {
        public async UniTask UseAction(CombatService combatService)
        {
            await UniTask.WaitForSeconds(GameplayConstants.DelayBeforeAction);
            await PerformAction(combatService);
            await UniTask.WaitForSeconds(GameplayConstants.DelayAfterAction);
        }
        
        public abstract bool CanUse(CombatService combatService);
        
        protected abstract UniTask PerformAction(CombatService combatService);
    }
}