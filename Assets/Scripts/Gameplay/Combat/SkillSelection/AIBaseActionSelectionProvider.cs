using Cysharp.Threading.Tasks;
using Gameplay.Units;

namespace Gameplay.Combat.SkillSelection
{
    public class AIBaseActionSelectionProvider : BaseActionSelectionProvider
    {
        private readonly UnitHealthData _unitHealthData;
        private readonly UnitStatsData _unitStatsData;

        public AIBaseActionSelectionProvider(UnitSkillsData unitSkillsData, UnitInventoryData unitInventoryData) :
            base(unitSkillsData, unitInventoryData)
        {
        }

        public override async UniTask<BaseCombatAction> SelectAction()
        {
            return UnitSkillsData.BasicAttackSkill;
        }
    }
}