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
    public class StatusEffectsApplicationService : IDisposable, IInitializable
    {
        private readonly StatusEffectsProcessor _statusEffectsProcessor;
        private readonly CombatEventsBus _combatEvents;

        private readonly CompositeDisposable _disposables = new();
        private readonly PlayerUnit _playerUnit;
        private readonly CombatData _combatData;

        [Inject]
        public StatusEffectsApplicationService(
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
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(_playerUnit, enemy, StatusEffectTriggerType.CombatStart);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(enemy, _playerUnit, StatusEffectTriggerType.CombatStart);
        }

        private void ProcessCombatEnded(IGameUnit enemy)
        {
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

        private void ProcessSkillUsed(SkillUsedData skillUsedData)
        {
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(skillUsedData.Attacker, skillUsedData.Target, StatusEffectTriggerType.SkillUsed);

            if (!skillUsedData.HitDataStream.ConsumeStance)
                return;
            
            var expirationType = StatusEffectsHelper.GetExpirationForDamageType(skillUsedData.HitDataStream.DamageType);
            _statusEffectsProcessor.RemoveStatusEffectsOnAction(skillUsedData.Attacker, expirationType);
        }

        private void ProcessHitDealt(HitEventData data)
        {
            var activeUnit = data.Attacker;
            var otherUnit = data.Target;

            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit, StatusEffectTriggerType.Hit);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit, StatusEffectTriggerType.DamageTaken);

            if (data.HitData.IsCritical)
                _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit, StatusEffectTriggerType.CriticalHit);

            var triggerType = StatusEffectsHelper.GetBuffTriggerForDamageType(data.HitData.DamageType);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit, triggerType);
            
            if(HealthHelper.DroppedBelowMediumHealth(data))
                _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit, StatusEffectTriggerType.MediumHealth);
            
            if(HealthHelper.DroppedBelowCriticalHealth(data))
                _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit, StatusEffectTriggerType.CriticalHealth);
        }

        private void ProcessHealed(HealEventData data)
        {
            var activeUnit = data.Target;
            var otherUnit = _combatData.OtherUnit;

            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit, StatusEffectTriggerType.ActiveUnitHealed);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit,activeUnit, StatusEffectTriggerType.OtherUnitHealed);
        }

        private void ProcessEvaded(EvadeEventData eventData)
        {
            var activeUnit = eventData.Attacker;
            var otherUnit = eventData.Target;

            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(activeUnit, otherUnit, StatusEffectTriggerType.Missed);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(otherUnit, activeUnit, StatusEffectTriggerType.Evaded);
        }
    }
}