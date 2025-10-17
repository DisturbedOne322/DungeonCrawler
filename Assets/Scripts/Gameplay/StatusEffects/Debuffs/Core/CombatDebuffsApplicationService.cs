using System;
using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using Helpers;
using Zenject;
using UniRx;
using UnityEngine;

namespace Gameplay.StatusEffects.Debuffs
{
    public class CombatDebuffsApplicationService : IDisposable, IInitializable
    {
        private readonly CombatDebuffsService _combatDebuffsService;
        private readonly CombatEventsBus _combatEvents;
        private readonly PlayerUnit _playerUnit;
        private readonly CombatData _combatData;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        public CombatDebuffsApplicationService(
            PlayerUnit playerUnit,
            CombatDebuffsService combatDebuffsService,
            CombatEventsBus combatEvents,
            CombatData combatData)
        {
            _playerUnit = playerUnit;
            _combatDebuffsService = combatDebuffsService;
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

            // _combatEvents.OnSkillUsed
            //     .Subscribe(ProcessSkillUsed)
            //     .AddTo(_disposables);

            _combatEvents.OnHealed
                .Subscribe(ProcessHealed)
                .AddTo(_disposables);

            _combatEvents.OnEvaded
                .Subscribe(ProcessEvaded)
                .AddTo(_disposables);
        }

        private void ProcessCombatStart(IGameUnit enemy)
        {
            Debug.Log(1);
            _combatDebuffsService.EnableDebuffsOnTrigger(_playerUnit, enemy, StatusEffectTriggerType.CombatStart);
            _combatDebuffsService.EnableDebuffsOnTrigger(enemy, _playerUnit, StatusEffectTriggerType.CombatStart);
        }

        private void ProcessCombatEnded(IGameUnit enemy)
        {
            _combatDebuffsService.ClearCombatDebuffs(_playerUnit);
            _combatDebuffsService.ClearCombatDebuffs(enemy);
        }

        private void ProcessTurnStarted(TurnData turn)
        {
            _combatDebuffsService.ProcessTurn(turn.ActiveUnit);
        }

        private void ProcessTurnEnded(TurnData turn)
        {
        }
        
        private void ProcessHitDealt(HitEventData data)
        {
            var attacker = data.Attacker;
            var target = data.Target;

            _combatDebuffsService.EnableDebuffsOnTrigger(attacker, target, StatusEffectTriggerType.Hit);
            _combatDebuffsService.EnableDebuffsOnTrigger(attacker, target, StatusEffectTriggerType.DamageTaken);

            if (data.HitData.IsCritical)
                _combatDebuffsService.EnableDebuffsOnTrigger(attacker, target, StatusEffectTriggerType.CriticalHit);

            var triggerType = StatusEffectsHelper.GetBuffTriggerForDamageType(data.HitData.DamageType);
            _combatDebuffsService.EnableDebuffsOnTrigger(attacker, target, triggerType);
        }

        private void ProcessHealed(HealEventData data)
        {
            var otherUnit = _combatData.OtherUnit;
            
            var healer = data.Target;
            
            _combatDebuffsService.EnableDebuffsOnTrigger(healer, otherUnit, StatusEffectTriggerType.ActiveUnitHealed);
            _combatDebuffsService.EnableDebuffsOnTrigger(otherUnit, healer, StatusEffectTriggerType.OtherUnitHealed);
        }

        private void ProcessEvaded(EvadeEventData eventData)
        {
            var attacker = eventData.Attacker;
            var target = eventData.Target;

            _combatDebuffsService.EnableDebuffsOnTrigger(attacker, target, StatusEffectTriggerType.Missed);
            _combatDebuffsService.EnableDebuffsOnTrigger(target, attacker, StatusEffectTriggerType.Evaded);
        }
    }
}