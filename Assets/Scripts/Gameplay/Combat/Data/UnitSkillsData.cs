using System.Collections.Generic;
using Gameplay.Combat.Skills;
using ModestTree;
using UniRx;

namespace Gameplay.Combat
{
    public class UnitSkillsData
    {
        public IReadOnlyReactiveCollection<BaseSkill> Skills => _skills;
        private readonly ReactiveCollection<BaseSkill> _skills = new();

        public void AssignSkills(List<BaseSkill> skills)
        {
            _skills.Clear();
            _skills.AllocFreeAddRange(skills);
        }

        public void AddSkill(BaseSkill skill) => _skills.Add(skill);
        public void RemoveSkill(BaseSkill skill) => _skills.Remove(skill);
    }
}