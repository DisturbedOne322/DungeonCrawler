using Gameplay.Skills.Core;
using ModestTree;
using UniRx;

namespace Gameplay.Units
{
    public class UnitSkillsContainer
    {
        private readonly ReactiveCollection<BaseSkill> _skills = new();
        
        public BaseSkill BasicAttackSkill { get; private set; }

        public BaseSkill GuardSkill { get; private set; }

        public IReadOnlyReactiveCollection<BaseSkill> Skills => _skills;

        public void AssignStartingSkills(UnitData data)
        {
            _skills.Clear();
            _skills.AllocFreeAddRange(data.SkillSet);

            BasicAttackSkill = data.BasicAttackSkill;
            GuardSkill = data.GuardSkill;
        }

        public void AddSkill(BaseSkill skill)
        {
            _skills.Add(skill);
        }

        public void RemoveSkill(BaseSkill skill)
        {
            _skills.Remove(skill);
        }

        public void SetNewBasicAttack(BaseSkill skill)
        {
            BasicAttackSkill = skill;
        }

        public void SetNewGuardSkill(BaseSkill skill)
        {
            GuardSkill = skill;
        }
    }
}