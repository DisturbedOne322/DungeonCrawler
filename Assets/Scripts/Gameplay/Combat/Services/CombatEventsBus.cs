using System;
using Gameplay.Combat.Data;
using Gameplay.Combat.Data.Events;
using Gameplay.Facades;
using UniRx;

namespace Gameplay.Combat.Services
{
    public class CombatEventsBus
    {
        private readonly Subject<IGameUnit> _onCombatEnded = new();
        private readonly Subject<IGameUnit> _onCombatStarted = new();
        private readonly Subject<EvadeEventData> _onEvaded = new();
        private readonly Subject<HealEventData> _onHealed = new();

        private readonly Subject<HitEventData> _onHitDealt = new();
        private readonly Subject<SkillCastedData> _onSkillCasted = new();
        private readonly Subject<SkillUsedData> _onSkillUsed = new();
        private readonly Subject<TurnData> _onTurnEnded = new();
        private readonly Subject<TurnData> _onTurnStarted = new();

        public IObservable<IGameUnit> OnCombatStarted => _onCombatStarted;
        public IObservable<IGameUnit> OnCombatEnded => _onCombatEnded;

        public IObservable<HitEventData> OnHitDealt => _onHitDealt;
        public IObservable<SkillCastedData> OnSkillCasted => _onSkillCasted;
        public IObservable<SkillUsedData> OnSkillUsed => _onSkillUsed;
        public IObservable<HealEventData> OnHealed => _onHealed;
        public IObservable<EvadeEventData> OnEvaded => _onEvaded;

        public IObservable<TurnData> OnTurnStarted => _onTurnStarted;
        public IObservable<TurnData> OnTurnEnded => _onTurnEnded;

        public void InvokeCombatStarted(IGameUnit enemy)
        {
            _onCombatStarted.OnNext(enemy);
        }

        public void InvokeCombatEnded(IGameUnit enemy)
        {
            _onCombatEnded.OnNext(enemy);
        }

        public void InvokeHitDealt(HitEventData data)
        {
            _onHitDealt.OnNext(data);
        }

        public void InvokeSkillCasted(SkillCastedData data)
        {
            _onSkillCasted.OnNext(data);
        }

        public void InvokeSkillUsed(SkillUsedData data)
        {
            _onSkillUsed.OnNext(data);
        }

        public void InvokeHealed(HealEventData data)
        {
            _onHealed.OnNext(data);
        }

        public void InvokeEvaded(EvadeEventData eventData)
        {
            _onEvaded.OnNext(eventData);
        }

        public void InvokeTurnStarted(TurnData data)
        {
            _onTurnStarted.OnNext(data);
        }

        public void InvokeTurnEnded(TurnData data)
        {
            _onTurnEnded.OnNext(data);
        }
    }
}