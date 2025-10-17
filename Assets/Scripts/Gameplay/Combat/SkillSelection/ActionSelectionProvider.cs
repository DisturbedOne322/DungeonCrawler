using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;

namespace Gameplay.Combat.SkillSelection
{
    public abstract class ActionSelectionProvider
    {
        protected UnitInventoryData UnitInventoryData;
        protected UnitSkillsData UnitSkillsData;

        public ActionSelectionProvider(UnitSkillsData unitSkillsData, UnitInventoryData unitInventoryData)
        {
            UnitSkillsData = unitSkillsData;
            UnitInventoryData = unitInventoryData;
        }

        public abstract UniTask<BaseCombatAction> SelectAction();
    }
}