using System;
using Gameplay.Combat.Data;
using UniRx;

namespace Gameplay.Combat.Services
{
    public class CombatSkillsUpdateService : IDisposable
    {
        private readonly IDisposable _subscription;
        
        public CombatSkillsUpdateService(CombatEventsBus combatEventsBus)
        {
            _subscription = combatEventsBus.OnTurnStarted.Subscribe(UpdateCooldowns);
        }
        
        public void Dispose()
        {
            _subscription.Dispose();
        }
        
        private void UpdateCooldowns(TurnData turnData)
        {
            var unit = turnData.ActiveUnit;
            var skillContainer = unit.UnitSkillsContainer;
            
            skillContainer.BasicAttackSkill.ReduceCooldown();
            skillContainer.GuardSkill.ReduceCooldown();

            foreach (var heldSkill in skillContainer.HeldSkills) 
                heldSkill.ReduceCooldown();
        }
    }
}