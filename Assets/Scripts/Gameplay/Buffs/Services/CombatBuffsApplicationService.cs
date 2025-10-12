using System;
using Gameplay.Buffs.Core;
using Gameplay.Combat.Data;
using Gameplay.Combat.Services;
using Gameplay.Facades;
using Gameplay.Units;
using Helpers;
using UniRx;
using Zenject;

namespace Gameplay.Buffs.Services
{
    public class CombatBuffsApplicationService : IDisposable, IInitializable
    {
        private readonly PlayerUnit _playerUnit;
        private readonly CombatBuffsService _combatBuffsService;
        private readonly CombatEventsBus _combatEvents;

        private readonly CompositeDisposable _disposables = new();

        [Inject]
        public CombatBuffsApplicationService(
            PlayerUnit playerUnit,
            CombatBuffsService combatBuffsService,
            CombatEventsBus combatEvents)
        {
            _playerUnit = playerUnit;
            _combatBuffsService = combatBuffsService;
            _combatEvents = combatEvents;
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
            _combatBuffsService.EnableBuffsOnTrigger(_playerUnit, BuffTriggerType.CombatStart);
            _combatBuffsService.EnableBuffsOnTrigger(enemy, BuffTriggerType.CombatStart);
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
            if(!skillUsedData.SkillData.ConsumeStance)
                return;
            
            var expirationType = BuffsHelper.GetExpirationForDamageType(skillUsedData.SkillData.DamageType); 
            _combatBuffsService.RemoveBuffsOnAction(skillUsedData.Attacker, expirationType);
        }

        private void ProcessHitDealt(HitEventData data)
        {
            var attacker = data.Attacker;
            var target = data.Target;

            _combatBuffsService.EnableBuffsOnTrigger(attacker, BuffTriggerType.Hit);
            _combatBuffsService.EnableBuffsOnTrigger(target, BuffTriggerType.DamageTaken);

            if (data.HitData.IsCritical)
                _combatBuffsService.EnableBuffsOnTrigger(attacker, BuffTriggerType.CriticalHit);

            var triggerType = BuffsHelper.GetBuffTriggerForDamageType(data.HitData.DamageType);
            _combatBuffsService.EnableBuffsOnTrigger(attacker, triggerType);
        }
        
        private void ProcessHealed(HealEventData data) => _combatBuffsService.EnableBuffsOnTrigger(data.Target, BuffTriggerType.Healing);

        private void ProcessEvaded(IGameUnit unit) => _combatBuffsService.EnableBuffsOnTrigger(unit, BuffTriggerType.Dodge);

        public void Dispose() => _disposables.Dispose();
    }
}