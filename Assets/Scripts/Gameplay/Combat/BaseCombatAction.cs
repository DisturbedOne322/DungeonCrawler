using Cysharp.Threading.Tasks;
using Data.Constants;
using Gameplay.Combat.AI;
using Gameplay.Combat.Services;

namespace Gameplay.Combat
{
    public abstract class BaseCombatAction : BaseGameItem
    {
        public async UniTask ExecuteAction(CombatService combatService)
        {
            await UniTask.WaitForSeconds(GameplayConstants.DelayBeforeAction);
            await PerformAction(combatService);
            await UniTask.WaitForSeconds(GameplayConstants.DelayAfterAction);
        }

        public virtual bool CanUse(CombatService combatService)
        {
            return true;
        }

        public abstract float EvaluateAction(AIActionEvaluationService evaluationService, AIContext context);

        protected abstract UniTask PerformAction(CombatService combatService);
    }
}