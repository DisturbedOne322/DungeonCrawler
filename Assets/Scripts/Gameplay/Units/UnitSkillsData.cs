using Gameplay.Skills.Core;
using ModestTree;
using UniRx;

namespace Gameplay.Units
{
    public class UnitSkillsData
    {
        public BaseSkill BasicAttackSkill { get; private set; }

        public BaseSkill GuardSkill { get; private set; }

        public ReactiveCollection<BaseSkill> Skills { get; } = new();

        public void AssignSkills(UnitData data)
        {
            Skills.Clear();
            Skills.AllocFreeAddRange(data.SkillSet);

            BasicAttackSkill = data.BasicAttackSkill;
            GuardSkill = data.GuardSkill;
        }

        public void AddSkill(BaseSkill skill)
        {
            Skills.Add(skill);
        }

        public void RemoveSkill(BaseSkill skill)
        {
            Skills.Remove(skill);
        }
    }
}