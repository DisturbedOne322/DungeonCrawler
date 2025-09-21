using System.Collections.Generic;
using Gameplay.Combat.Skills;
using Gameplay.Units;
using ModestTree;
using UniRx;

namespace Gameplay.Combat.Data
{
    public class UnitSkillsData
    {
        private BaseSkill _basicAttackSkill;
        public BaseSkill BasicAttackSkill => _basicAttackSkill;

        private BaseSkill _guardSkill;
        public BaseSkill GuardSkill => _guardSkill;
        
        public IReadOnlyReactiveCollection<BaseSkill> Skills => _skills;
        private readonly ReactiveCollection<BaseSkill> _skills = new();

        public void AssignSkills(UnitData data)
        {
            _skills.Clear();
            _skills.AllocFreeAddRange(data.SkillSet);
            
            _basicAttackSkill = data.BasicAttackSkill;
            _guardSkill = data.GuardSkill;
        }

        public void AddSkill(BaseSkill skill) => _skills.Add(skill);
        public void RemoveSkill(BaseSkill skill) => _skills.Remove(skill);
    }
}