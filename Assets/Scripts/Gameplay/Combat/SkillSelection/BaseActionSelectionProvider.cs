using Cysharp.Threading.Tasks;
using Gameplay.Facades;

namespace Gameplay.Combat.SkillSelection
{
    public abstract class BaseActionSelectionProvider
    {
        protected IGameUnit Unit;

        public BaseActionSelectionProvider(IGameUnit unit)
        {
            Unit = unit;
        }

        public abstract UniTask<BaseCombatAction> SelectAction();
    }
}