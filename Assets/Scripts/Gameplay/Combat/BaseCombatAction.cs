using Constants;
using Cysharp.Threading.Tasks;

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
        
        public virtual bool CanUse(CombatService combatService) => true;
        
        protected abstract UniTask PerformAction(CombatService combatService);
    }
}