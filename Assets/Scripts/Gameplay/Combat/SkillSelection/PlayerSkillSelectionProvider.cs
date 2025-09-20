using Cysharp.Threading.Tasks;
using Gameplay.Combat.Skills;

namespace Gameplay.Combat
{
    public class PlayerSkillSelectionProvider : SkillSelectionProvider
    {
        public PlayerSkillSelectionProvider(UnitSkillsData unitSkillsData) : base(unitSkillsData)
        {
        }

        public override async UniTask<BaseSkill> SelectSkillToUse()
        {
            await UniTask.WaitForSeconds(1);
            return new BaseAttackSkill();
        }
    }
}