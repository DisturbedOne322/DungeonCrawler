using System;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.Units;
using UniRx;

namespace Gameplay.Combat
{
    public class CombatCleanupService : IDisposable
    {
        private readonly PlayerUnit _player;
        private readonly StatusEffectsProcessor _statusEffectsProcessor;
        
        private readonly IDisposable _subscription;

        public CombatCleanupService(PlayerUnit player, StatusEffectsProcessor statusEffectsProcessor,
            CombatEventsBus combatEventsBus)
        {
            _player = player;
            _statusEffectsProcessor = statusEffectsProcessor;
            
            _subscription = combatEventsBus.OnCombatEnded.Subscribe(Cleanup);
        }

        private void Cleanup(IGameUnit enemy)
        {
            ResetUnit(_player);
            ResetUnit(enemy);
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void ResetUnit(IGameUnit unit)
        {
            ResetCooldowns(unit);
            _statusEffectsProcessor.ClearAllStatusEffects(unit);
        }

        private void ResetCooldowns(IGameUnit unit)
        {
            var skillsContainer = unit.UnitSkillsContainer;
            
            skillsContainer.BasicAttackSkill.ResetCooldown();
            skillsContainer.GuardSkill.ResetCooldown();
            
            var skillsHeld = skillsContainer.HeldSkills;
            
            foreach(var skillHeld in skillsHeld)
                skillHeld.ReduceCooldown();
        }
    }
}