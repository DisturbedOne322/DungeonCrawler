using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;

namespace Gameplay.Combat.SkillSelection
{
    public abstract class SkillSelectionProvider
    {
        protected UnitSkillsData UnitSkillsData;
        
        public SkillSelectionProvider(UnitSkillsData unitSkillsData) => UnitSkillsData = unitSkillsData;

        public abstract UniTask<BaseSkill> SelectSkillToUse();
    }
}