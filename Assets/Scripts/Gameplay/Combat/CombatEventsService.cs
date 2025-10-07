using Gameplay.Combat.Data;
using Gameplay.Facades;
using UniRx;

namespace Gameplay.Combat
{
    public class CombatEventsService
    {
        public Subject<IGameUnit> OnCombatStarted = new();
        public Subject<IGameUnit> OnCombatEnded = new();
        
        public Subject<HitEventData> OnHitDealt = new();
        public Subject<HealEventData> OnHealed = new();
        
        public Subject<TurnData> OnTurnStarted = new();
        public Subject<TurnData> OnTurnEnded = new();

        public Subject<IGameUnit> OnEvaded = new();

        public void InvokeCombatStarted(IGameUnit enemy) => OnCombatStarted?.OnNext(enemy);
        public void InvokeCombatEnded(IGameUnit enemy) => OnCombatEnded?.OnNext(enemy);
        
        public void InvokeHitDealt(HitEventData eventData) => OnHitDealt?.OnNext(eventData);
        public void InvokeHealed(HealEventData eventData) => OnHealed?.OnNext(eventData);
        
        public void InvokeTurnStarted(TurnData turnData) => OnTurnStarted?.OnNext(turnData);
        public void InvokeTurnEnded(TurnData turnData) => OnTurnEnded?.OnNext(turnData);
        
        public void InvokeEvaded(IGameUnit evadedUnit) => OnEvaded?.OnNext(evadedUnit);
    }
}