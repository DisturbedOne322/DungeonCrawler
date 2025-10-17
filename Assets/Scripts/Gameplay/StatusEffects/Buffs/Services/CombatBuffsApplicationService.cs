using System;
using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using Helpers;
using UniRx;
using Zenject;

namespace Gameplay.StatusEffects.Buffs.Services
{
    public class CombatBuffsApplicationService : IDisposable, IInitializable
    {
        private readonly CombatBuffsService _combatBuffsService;
        private readonly CombatEventsBus _combatEvents;

        private readonly CompositeDisposable _disposables = new();
        private readonly PlayerUnit _playerUnit;
        private readonly CombatData _combatData;

        [Inject]
        public CombatBuffsApplicationService(
            PlayerUnit playerUnit,
            CombatBuffsService combatBuffsService,
            CombatEventsBus combatEvents,
            CombatData combatData)
        {
            _playerUnit = playerUnit;
            _combatBuffsService = combatBuffsService;
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
            _combatBuffsService.EnableBuffsOnTrigger(_playerUnit, StatusEffectTriggerType.CombatStart);
            _combatBuffsService.EnableBuffsOnTrigger(enemy, StatusEffectTriggerType.CombatStart);
        }

        private void ProcessCombatEnded(IGameUnit enemy)
        {
            _combatBuffsService.ClearCombatBuffs(_playerUnit);
            _combatBuffsService.ClearCombatBuffs(enemy);
        }

        private void ProcessTurnStarted(TurnData turn)
        {
            _combatBuffsService.ProcessTurn(turn.ActiveUnit);
        }

        private void ProcessTurnEnded(TurnData turn)
        {
        }

        private void ProcessSkillUsed(SkillUsedData skillUsedData)
        {
            if (!skillUsedData.SkillData.ConsumeStance)
                return;

            var expirationType = StatusEffectsHelper.GetExpirationForDamageType(skillUsedData.SkillData.DamageType);
            _combatBuffsService.RemoveBuffsOnAction(skillUsedData.Attacker, expirationType);
        }

        private void ProcessHitDealt(HitEventData data)
        {
            var attacker = data.Attacker;
            var target = data.Target;

            _combatBuffsService.EnableBuffsOnTrigger(attacker, StatusEffectTriggerType.Hit);
            _combatBuffsService.EnableBuffsOnTrigger(target, StatusEffectTriggerType.DamageTaken);

            if (data.HitData.IsCritical)
                _combatBuffsService.EnableBuffsOnTrigger(attacker, StatusEffectTriggerType.CriticalHit);

            var triggerType = StatusEffectsHelper.GetBuffTriggerForDamageType(data.HitData.DamageType);
            _combatBuffsService.EnableBuffsOnTrigger(attacker, triggerType);
        }

        private void ProcessHealed(HealEventData data)
        {
            var otherUnit = _combatData.OtherUnit;
            
            var healer = data.Target;
            
            _combatBuffsService.EnableBuffsOnTrigger(healer, StatusEffectTriggerType.ActiveUnitHealed);
            _combatBuffsService.EnableBuffsOnTrigger(otherUnit, StatusEffectTriggerType.OtherUnitHealed);
        }

        private void ProcessEvaded(EvadeEventData eventData)
        {
            var attacker = eventData.Attacker;
            var target = eventData.Target;

            _combatBuffsService.EnableBuffsOnTrigger(attacker, StatusEffectTriggerType.Missed);
            _combatBuffsService.EnableBuffsOnTrigger(target, StatusEffectTriggerType.Evaded);
        }
    }
}