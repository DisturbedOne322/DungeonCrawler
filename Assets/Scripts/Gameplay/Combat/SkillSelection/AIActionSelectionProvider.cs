using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;

namespace Gameplay.Combat.SkillSelection
{
    public class AIActionSelectionProvider : ActionSelectionProvider
    {
        private readonly UnitHealthData _unitHealthData;
        private readonly UnitStatsData _unitStatsData;
        
        public AIActionSelectionProvider(UnitSkillsData unitSkillsData, UnitInventoryData unitInventoryData) : 
            base(unitSkillsData, unitInventoryData)
        {
            
        }

        public override async UniTask<BaseCombatAction> SelectAction()
        {
            return UnitSkillsData.BasicAttackSkill;
        }
    }
}