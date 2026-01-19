using Gameplay.Combat.Services;

namespace Gameplay.Skills.Core
{
    public class SkillHeldData
    {
        private readonly BaseSkill _skill;
        private int _cooldown = 0;
        
        public BaseSkill Skill => _skill;
        public int Cooldown => _cooldown;
        
        public SkillHeldData(BaseSkill skill) => _skill = skill;

        public bool CanUse(CombatService combatService) => _cooldown <= 0 && _skill.CanUse(combatService);

        public void SetCooldown() => _cooldown = _skill.Cooldown + 1;

        public void ResetCooldown() => _cooldown = 0;

        public void ReduceCooldown(int amount = 1)
        {
            _cooldown -= amount;
            if(_cooldown < 0)
                _cooldown = 0;
        }
    }
}