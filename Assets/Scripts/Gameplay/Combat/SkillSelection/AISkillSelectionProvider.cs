using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;

namespace Gameplay.Combat
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
            await UniTask.WaitForSeconds(1);
            return new BaseAttackSkill();
            var attackSkill = UnitSkillsData.Skills[0];
            return attackSkill;
        }
    }
}