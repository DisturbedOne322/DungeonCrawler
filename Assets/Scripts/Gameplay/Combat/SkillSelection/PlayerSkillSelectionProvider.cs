using Cysharp.Threading.Tasks;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
using UI;

namespace Gameplay.Combat.SkillSelection
{
    public class PlayerSkillSelectionProvider : SkillSelectionProvider
    {
        private readonly UIFactory _factory;
        
        public PlayerSkillSelectionProvider(UnitSkillsData unitSkillsData, UIFactory uiFactory) : base(unitSkillsData)
        {
            _factory = uiFactory;
        }

        public override async UniTask<BaseSkill> SelectSkillToUse()
        {
            var popup = _factory.CreateSkillSelectPopup();

            var tcs = new UniTaskCompletionSource<BaseSkill>();

            popup.OnSkillSelected += (skill) => tcs.TrySetResult(skill);

            BaseSkill selectedSkill = await tcs.Task;

            return selectedSkill;
        }
    }
}