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
        private readonly StatusEffectsProcessor _statusEffectsProcessor;

        private readonly CompositeDisposable _disposables = new();
        private readonly PlayerUnit _playerUnit;

        [Inject]
        public CombatStatusEffectsApplicationService(
            PlayerUnit playerUnit,
            CombatEventsBus combatEvents,
            CombatData combatData,
            StatusEffectsProcessor statusEffectsProcessor)
        {
            _playerUnit = playerUnit;
            _combatEvents = combatEvents;
            _combatData = combatData;
            _statusEffectsProcessor = statusEffectsProcessor;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void Initialize()
        {
            Bind(_combatEvents.OnCombatStarted, ProcessCombatStart);
            Bind(_combatEvents.OnCombatEnded, ProcessCombatEnded);
            Bind(_combatEvents.OnTurnStarted, ProcessTurnStarted);
            Bind(_combatEvents.OnTurnEnded, ProcessTurnEnded);
            Bind(_combatEvents.OnHitDealt, ProcessHitDealt);
            Bind(_combatEvents.OnSkillCasted, ProcessSkillCasted);
            Bind(_combatEvents.OnSkillUsed, ProcessSkillUsed);
            Bind(_combatEvents.OnHealed, ProcessHealed);
            Bind(_combatEvents.OnEvaded, ProcessEvaded);
        }
        
        private void Bind<T>(IObservable<T> stream, Action<T> handler)
        {
            stream.Subscribe(handler).AddTo(_disposables);
        }
        
        private void ProcessCombatStart(IGameUnit enemy)
        {
            TriggerBoth(_playerUnit, enemy, 
                StatusEffectTriggerType.OnCombatStart, StatusEffectTriggerType.OnCombatStart);
        }

        private void ProcessCombatEnded(IGameUnit enemy)
        {
            TriggerBoth(_playerUnit, enemy,  StatusEffectTriggerType.OnCombatEnd, 
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
            TriggerBoth(
                skillCastedData.Attacker,
                skillCastedData.Target,
                StatusEffectTriggerType.OnSkillCasted,
                StatusEffectTriggerType.OnOtherSkillCasted);
        }
        
        private void ProcessSkillUsed(SkillUsedData skillUsedData)
        {
            TriggerBoth(
                skillUsedData.Attacker,
                skillUsedData.Target,
                StatusEffectTriggerType.OnSkillUsed,
                StatusEffectTriggerType.OnOtherSkillUsed);

            if (!skillUsedData.HitDataStream.ConsumeStance)
                return;

            var expiration = StatusEffectsHelper
                .GetExpirationForDamageType(skillUsedData.HitDataStream.DamageType);

            _statusEffectsProcessor.RemoveStatusEffectsOnAction(skillUsedData.Attacker, expiration);
        }

        private void ProcessHitDealt(HitEventData data)
        {
            var active = data.Attacker;
            var other = data.Target;

            TriggerBoth(active, other,
                StatusEffectTriggerType.OnHit,
                StatusEffectTriggerType.OnDamageTaken);

            if (data.HitData.IsCritical)
            {
                TriggerBoth(active, other,
                    StatusEffectTriggerType.OnCriticalHit,
                    StatusEffectTriggerType.OnCriticalDamageTaken);
            }

            TriggerBoth(active, other,
                StatusEffectsHelper.GetBuffTriggerForDamageTypeDealt(data.HitData.DamageType),
                StatusEffectsHelper.GetBuffTriggerForDamageTypeTaken(data.HitData.DamageType));

            ProcessHealthThresholds(data, active, other);
        }

        private void ProcessHealed(HealEventData data)
        {
            var activeUnit = data.Target;
            var otherUnit = _combatData.OtherUnit;
            
            TriggerBoth(activeUnit, otherUnit, 
                StatusEffectTriggerType.OnHealed, StatusEffectTriggerType.OnOtherHealed);
        }

        private void ProcessEvaded(EvadeEventData eventData) => 
            TriggerBoth(eventData.Attacker, eventData.Target, 
                StatusEffectTriggerType.OnMissed, StatusEffectTriggerType.OnEvaded);

        private void ProcessHealthThresholds(
            HitEventData data,
            ICombatant active,
            ICombatant other)
        {
            if (HealthHelper.DroppedBelowMediumHealth(data))
            {
                TriggerBoth(active, other,
                    StatusEffectTriggerType.OnOtherMediumHealth,
                    StatusEffectTriggerType.OnMediumHealth);
            }

            if (HealthHelper.DroppedBelowCriticalHealth(data))
            {
                TriggerBoth(active, other,
                    StatusEffectTriggerType.OnOtherCriticalHealth,
                    StatusEffectTriggerType.OnCriticalHealth);
            }
        }
        
        private void TriggerBoth(
            ICombatant active,
            ICombatant other,
            StatusEffectTriggerType activeTrigger,
            StatusEffectTriggerType otherTrigger)
        {
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(active, other, activeTrigger);
            _statusEffectsProcessor.EnableStatusEffectsOnTrigger(other, active, otherTrigger);
        }
    }
}