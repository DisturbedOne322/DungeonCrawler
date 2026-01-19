using Gameplay.Skills.Core;
using ModestTree;
using UniRx;

namespace Gameplay.Units
{
    public class UnitSkillsContainer
    {
        private readonly ReactiveCollection<SkillHeldData> _heldSkills = new();
        
        public SkillHeldData BasicAttackSkill { get; private set; }

        public SkillHeldData GuardSkill { get; private set; }

        public IReadOnlyReactiveCollection<SkillHeldData> HeldSkills => _heldSkills;

        public void AssignStartingSkills(UnitData data)
        {
            _heldSkills.Clear();

            foreach (var baseSkill in data.SkillSet) 
                _heldSkills.Add(new (baseSkill));

            BasicAttackSkill = new(data.BasicAttackSkill);
            GuardSkill = new(data.GuardSkill);
        }

        public void AddSkill(BaseSkill skill)
        {
            _heldSkills.Add(new(skill));
        }

        public void RemoveSkill(BaseSkill skill)
        {
            _heldSkills.Remove(new(skill));
        }

        public void SetNewBasicAttack(BaseSkill skill)
        {
            BasicAttackSkill = new(skill);
        }

        public void SetNewGuardSkill(BaseSkill skill)
        {
            GuardSkill = new(skill);
        }
    }
}