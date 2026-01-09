using System;
using Gameplay.Combat.Data;
using Gameplay.Combat.Data.Events;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using Helpers;
using UniRx;
using Zenject;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class CombatStatusEffectsApplicationService : IDisposable, IInitializable
    {
        private readonly CombatData _combatData;
        private readonly CombatEventsBus _combatEvents;

        private readonly CompositeDisposable _disposables = new();
        private readonly PlayerUnit _playerUnit;
        private readonly StatusEffectsProcessor _statusEffectsProcessor;

        [Inject]
        public CombatStatusEffectsApplicationService(
            PlayerUnit playerUnit,
            StatusEffectsProcessor statusEffectsProcessor,
            CombatEventsBus combatEvents,
            CombatData combatData)
        {
            _playerUnit = playerUnit;
            _statusEffectsProcessor = statusEffectsProcessor;
            _combatEvents = combatEvents;
            _combatData = combatData;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void Initialize()
        {
            _combatEvents.OnCombatStarted
                .Subscribe(ProcessCombatStart)
                .AddTo(_disposables);

            _combatEvents.OnCombatEnded
                .Subscribe(ProcessCombatEnded)
                .AddTo(_disposables);

            _combatEvents.OnTurnStarted
                .Subscribe(ProcessTurnStarted)
                .AddTo(_disposables);

            _combatEvents.OnTurnEnded
                .Subscribe(ProcessTurnEnded)
                .AddTo(_disposables);

            _combatEvents.OnHitDealt
                .Subscribe(ProcessHitDealt)
                .AddTo(_disposables);

            _combatEvents.OnSkillCasted
                .Subscribe(ProcessSkillCasted)
                .AddTo(_disposables);
            
            _combatEvents.OnSkillUsed
                .Subscribe(ProcessSkillUsed)
                .AddTo(_disposables);

            _combatEvents.OnHealed
                .Subscribe(ProcessHealed)
                .AddTo(_disposables);

            _combatEvents.OnEvaded
                .Subscribe(ProcessEvaded)
                .AddTo(_disposables);
        }
        
        private void ProcessCombatStart(IGameUnit enemy)
        {
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(_playerUnit, enemy,
                StatusEffectTriggerType.OnCombatStart);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(enemy, _playerUnit,
                StatusEffectTriggerType.OnCombatStart);
        }

        private void ProcessCombatEnded(IGameUnit enemy)
        {
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(_playerUnit, enemy,
                StatusEffectTriggerType.OnCombatEnd);
            
            _statusEffectsProcessor.ClearAllStatusEffects(_playerUnit);
            _statusEffectsProcessor.ClearAllStatusEffects(enemy);
        }

        private void ProcessTurnStarted(TurnData turn)
        {
            _statusEffectsProcessor.ProcessTurn(turn.ActiveUnit);
        }

        private void ProcessTurnEnded(TurnData turn)
        {
            
        }

        private void ProcessSkillCasted(SkillCastedData skillCastedData)
        {
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(skillCastedData.Attacker, skillCastedData.Target,
                StatusEffectTriggerType.OnSkillCasted);
            
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(skillCastedData.Target, skillCastedData.Attacker,
                StatusEffectTriggerType.OnOtherSkillCasted);

            if (skillCastedData.HitDataStream.ConsumeStance)
            {
                var expirationType = StatusEffectsHelper.GetExpirationForDamageType(skillCastedData.HitDataStream.DamageType);
                _statusEffectsProcessor.RemoveStatusEffectsOnAction(skillCastedData.Attacker, expirationType);
            }
        }
        
        private void ProcessSkillUsed(SkillUsedData skillUsedData)
        {
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(skillUsedData.Attacker, skillUsedData.Target,
                StatusEffectTriggerType.OnSkillUsed);
            
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(skillUsedData.Target, skillUsedData.Attacker,
                StatusEffectTriggerType.OnOtherSkillUsed);

            if (skillUsedData.HitDataStream.ConsumeStance)
            {
                var expirationType = StatusEffectsHelper.GetExpirationForDamageType(skillUsedData.HitDataStream.DamageType);
                _statusEffectsProcessor.RemoveStatusEffectsOnAction(skillUsedData.Attacker, expirationType);
            }
        }

        private void ProcessHitDealt(HitEventData data)
        {
            var activeUnit = data.Attacker;
            var otherUnit = data.Target;

            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit, StatusEffectTriggerType.OnHit);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit,
                StatusEffectTriggerType.OnDamageTaken);

            if (data.HitData.IsCritical)
            {
                _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit,
                    StatusEffectTriggerType.OnCriticalHit);
                _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit,
                    StatusEffectTriggerType.OnCriticalDamageTaken);
            }

            var doneTriggerType = StatusEffectsHelper.GetBuffTriggerForDamageTypeDealt(data.HitData.DamageType);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit, doneTriggerType);
            
            var takenTriggerType = StatusEffectsHelper.GetBuffTriggerForDamageTypeTaken(data.HitData.DamageType);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit, takenTriggerType);

            if (HealthHelper.DroppedBelowMediumHealth(data))
            {
                _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit,
                    StatusEffectTriggerType.OnOtherMediumHealth);
                
                _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit,
                    StatusEffectTriggerType.OnMediumHealth);
            }
            
            if (HealthHelper.DroppedBelowCriticalHealth(data))
            {
                _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit,
                    StatusEffectTriggerType.OnOtherCriticalHealth);
                
                _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit,
                    StatusEffectTriggerType.OnCriticalHealth);   
            }
        }

        private void ProcessHealed(HealEventData data)
        {
            var activeUnit = data.Target;
            var otherUnit = _combatData.OtherUnit;

            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit,
                StatusEffectTriggerType.OnHealed);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit,
                StatusEffectTriggerType.OnOtherHealed);
        }

        private void ProcessEvaded(EvadeEventData eventData)
        {
            var activeUnit = eventData.Attacker;
            var otherUnit = eventData.Target;

            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit, StatusEffectTriggerType.OnMissed);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit, StatusEffectTriggerType.OnEvaded);
        }
    }
}