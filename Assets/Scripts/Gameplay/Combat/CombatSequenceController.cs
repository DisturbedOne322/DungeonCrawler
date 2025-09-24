using Cysharp.Threading.Tasks;
using Gameplay.Units;
using UniRx;

namespace Gameplay.Combat
{
    public class CombatSequenceController
    {
        private readonly CombatData _combatData;
        private readonly CombatService _combatService;
        
        public readonly Subject<Unit> OnCombatStarted = new ();
        public readonly Subject<Unit> OnCombatEnded = new ();
        
        public CombatSequenceController(CombatData combatData, CombatService combatService)
        {
            _combatData = combatData;
            _combatService = combatService;
        }

        public async UniTask StartCombat(GameUnit enemyUnit)
        {
            _combatService.StartCombat(enemyUnit);
            OnCombatStarted.OnNext(default);
            
            while (!IsCombatOver())
            {
                _combatService.StartTurn();
                await ProcessTurn();
            }
            
            OnCombatEnded.OnNext(default);
        }

        private async UniTask ProcessTurn()
        {
            var currentUnit = _combatData.ActiveUnit;

            var action = await currentUnit.ActionSelectionProvider.SelectAction();
            await action.UseAction(_combatService);
        }
        
        private bool IsCombatOver() => IsUnitDead(_combatData.Enemy) || IsUnitDead(_combatData.Player);

        private bool IsUnitDead(GameUnit unit) => unit.HealthData.IsDead.Value;
    }
}