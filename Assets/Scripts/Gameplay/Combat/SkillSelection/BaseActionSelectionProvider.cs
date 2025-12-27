using Cysharp.Threading.Tasks;
using Gameplay.Units;

namespace Gameplay.Combat.SkillSelection
{
    public abstract class BaseActionSelectionProvider
    {
        protected UnitInventoryData UnitInventoryData;
        protected UnitSkillsData UnitSkillsData;

        public BaseActionSelectionProvider(UnitSkillsData unitSkillsData, UnitInventoryData unitInventoryData)
        {
            UnitSkillsData = unitSkillsData;
            UnitInventoryData = unitInventoryData;
        }

        public abstract UniTask<BaseCombatAction> SelectAction();
    }
}