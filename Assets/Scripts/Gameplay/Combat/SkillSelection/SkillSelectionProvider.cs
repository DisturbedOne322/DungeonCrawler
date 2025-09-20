using Cysharp.Threading.Tasks;
using Gameplay.Combat.Skills;

namespace Gameplay.Combat
{
    public abstract class SkillSelectionProvider
    {
        protected UnitSkillsData UnitSkillsData;
        
        public SkillSelectionProvider(UnitSkillsData unitSkillsData) => UnitSkillsData = unitSkillsData;

        public abstract UniTask<BaseSkill> SelectSkillToUse();
    }
}