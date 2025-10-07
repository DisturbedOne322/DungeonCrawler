using System;
using Gameplay.Combat;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using Gameplay.Units;
using UniRx;
using Zenject;

namespace Gameplay.Buffs
{
    public class CombatBuffsApplicationService : IDisposable, IInitializable
    {
        private readonly PlayerUnit _playerUnit;
        private readonly CombatBuffsService _combatBuffsService;
        private readonly CombatEventsService _combatEventsService;

        private CompositeDisposable _disposables = new();
        
        public CombatBuffsApplicationService(
            PlayerUnit playerUnit,
            CombatBuffsService combatBuffsService,
            CombatEventsService combatEventsService)
        {
            _playerUnit = playerUnit;
            _combatBuffsService = combatBuffsService;
            _combatEventsService = combatEventsService;
        }

        public void Initialize()
        {
            _combatEventsService.OnCombatStarted.Subscribe(ProcessCombatStart);
            _combatEventsService.OnCombatEnded.Subscribe(ProcessCombatEnded);
            
            _combatEventsService.OnTurnStarted.Subscribe(ProcessTurnStarted);
            _combatEventsService.OnTurnEnded.Subscribe(ProcessTurnEnded);

            _combatEventsService.OnHitDealt.Subscribe(ProcessHitDealt);
            
            _combatEventsService.OnHealed.Subscribe(ProcessHealed);
            
            _combatEventsService.OnEvaded.Subscribe(ProcessEvaded);
        }

        private void ProcessCombatStart(IGameUnit enemy)
        {
            _combatBuffsService.EnableBuffsOnTrigger(_playerUnit, BuffTriggerType.CombatStart);
            _combatBuffsService.EnableBuffsOnTrigger(enemy, BuffTriggerType.CombatStart);
        }
        
        private void ProcessCombatEnded(IGameUnit enemy)
        {
            _combatBuffsService.ClearCombatBuffs(_playerUnit);
            _combatBuffsService.ClearCombatBuffs(enemy);
        }
        
        private void ProcessTurnStarted(TurnData turnData)
        {
            if(turnData.TurnCount <= 1)
                _combatBuffsService.ApplyPermanentBuffs(turnData.ActiveUnit);
            
            _combatBuffsService.ProcessTurn(turnData.ActiveUnit);
        }
        
        private void ProcessTurnEnded(TurnData turnData)
        {
            
        }
        
        private void ProcessHitDealt(HitEventData eventData)
        {
            var attacker = eventData.Attacker;
            
            _combatBuffsService.EnableBuffsOnTrigger(attacker, BuffTriggerType.Hit);
            
            _combatBuffsService.EnableBuffsOnTrigger(eventData.Target, BuffTriggerType.DamageTaken);
            
            if(eventData.IsCritical)
                _combatBuffsService.EnableBuffsOnTrigger(attacker, BuffTriggerType.CriticalHit);

            if (eventData.HitDamageType == HitDamageType.Physical)
            {
                _combatBuffsService.EnableBuffsOnTrigger(attacker, BuffTriggerType.PhysicalDamage);
                _combatBuffsService.RemoveBuffsOnAction(attacker, BuffExpirationType.NextPhysicalAction);
            }

            if (eventData.HitDamageType == HitDamageType.Magical)
            {
                _combatBuffsService.EnableBuffsOnTrigger(attacker, BuffTriggerType.MagicalDamage);
                _combatBuffsService.RemoveBuffsOnAction(attacker, BuffExpirationType.NextPhysicalAction);
            }

            if (eventData.HitDamageType == HitDamageType.Absolute)
            {
                _combatBuffsService.EnableBuffsOnTrigger(attacker, BuffTriggerType.AbsoluteDamage);
                _combatBuffsService.RemoveBuffsOnAction(attacker, BuffExpirationType.NextAbsoluteAction);
            }
        }
        
        private void ProcessHealed(HealEventData eventData)
        {
            _combatBuffsService.EnableBuffsOnTrigger(eventData.Target, BuffTriggerType.Healing);
        }
        
        private void ProcessEvaded(IGameUnit evadedUnit)
        {
            _combatBuffsService.EnableBuffsOnTrigger(evadedUnit, BuffTriggerType.Dodge);
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}