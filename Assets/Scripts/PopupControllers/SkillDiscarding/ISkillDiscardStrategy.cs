using Cysharp.Threading.Tasks;
using Gameplay.Skills.Core;

namespace PopupControllers.SkillDiscarding
{
    public interface ISkillDiscardStrategy
    {
        UniTask<BaseSkill> MakeSkillDiscardChoice(BaseSkill newSkill);
    }
}