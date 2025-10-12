using System;
using Gameplay.Combat.Data;
using Gameplay.Facades;
using UniRx;

namespace Gameplay.Combat.Services
{
    public class CombatEventsBus
    {
        private readonly Subject<IGameUnit> _onCombatStarted = new();
        private readonly Subject<IGameUnit> _onCombatEnded = new();
        
        private readonly Subject<HitEventData> _onHitDealt = new();
        private readonly Subject<SkillUsedData> _onSkillUsed = new();
        private readonly Subject<HealEventData> _onHealed = new();
        private readonly Subject<IGameUnit> _onEvaded = new();

        private readonly Subject<TurnData> _onTurnStarted = new();
        private readonly Subject<TurnData> _onTurnEnded = new();
        
        public IObservable<IGameUnit> OnCombatStarted => _onCombatStarted;
        public IObservable<IGameUnit> OnCombatEnded => _onCombatEnded;
        
        public IObservable<HitEventData> OnHitDealt => _onHitDealt;
        public IObservable<SkillUsedData> OnSkillUsed => _onSkillUsed;
        public IObservable<HealEventData> OnHealed => _onHealed;
        public IObservable<IGameUnit> OnEvaded => _onEvaded;

        public IObservable<TurnData> OnTurnStarted => _onTurnStarted;
        public IObservable<TurnData> OnTurnEnded => _onTurnEnded;
        

        public void InvokeCombatStarted(IGameUnit enemy) => _onCombatStarted.OnNext(enemy);
        public void InvokeCombatEnded(IGameUnit enemy) => _onCombatEnded.OnNext(enemy);
        
        public void InvokeHitDealt(HitEventData data) => _onHitDealt.OnNext(data);
        public void InvokeSkillUsed(SkillUsedData data) => _onSkillUsed.OnNext(data);
        public void InvokeHealed(HealEventData data) => _onHealed.OnNext(data);
        public void InvokeEvaded(IGameUnit unit) => _onEvaded.OnNext(unit);
        
        public void InvokeTurnStarted(TurnData data) => _onTurnStarted.OnNext(data);
        public void InvokeTurnEnded(TurnData data) => _onTurnEnded.OnNext(data);
    }
}