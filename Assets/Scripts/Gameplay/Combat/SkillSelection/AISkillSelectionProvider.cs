using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;

namespace Gameplay.Combat.SkillSelection
{
    public class AISkillSelectionProvider : SkillSelectionProvider
    {
        private readonly UnitHealthData _unitHealthData;
        private readonly UnitStatsData _unitStatsData;
        
        public AISkillSelectionProvider(UnitSkillsData unitSkillsData) : base(unitSkillsData)
        {
            
        }

        public override async UniTask<BaseSkill> SelectSkillToUse()
        {
            return UnitSkillsData.BasicAttackSkill;
        }
    }
}