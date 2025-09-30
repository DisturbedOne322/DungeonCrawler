using Cysharp.Threading.Tasks;
using Gameplay.Experience;
using Gameplay.Facades;
using Gameplay.Units;
using UniRx;

namespace Gameplay.Combat
{
    public class CombatSequenceController
    {
        private readonly CombatData _combatData;
        private readonly CombatService _combatService;
        private readonly PlayerExperienceService _playerExperienceService;
        
        public readonly Subject<Unit> OnCombatStarted = new ();
        public readonly Subject<Unit> OnCombatEnded = new ();
        public readonly Subject<int> OnEnemyDefeated = new();
        
        public CombatSequenceController(CombatData combatData, CombatService combatService,
            PlayerExperienceService playerExperienceService)
        {
            _combatData = combatData;
            _combatService = combatService;
            _playerExperienceService = playerExperienceService;
        }

        public async UniTask StartCombat(EnemyUnit enemyUnit)
        {
            _combatService.StartCombat(enemyUnit);
            OnCombatStarted.OnNext(default);
            
            while (!IsCombatOver())
            {
                _combatService.StartTurn();
                await ProcessTurn();
            }
            
            OnCombatEnded.OnNext(default);
            
            if(IsUnitDead(enemyUnit))
                _playerExperienceService.AddExperience(enemyUnit.ExperienceBonus);
        }

        private async UniTask ProcessTurn()
        {
            var currentUnit = _combatData.ActiveUnit;

            var action = await currentUnit.ActionSelectionProvider.SelectAction();
            await action.UseAction(_combatService);
        }
        
        private bool IsCombatOver() => IsUnitDead(_combatData.Enemy) || IsUnitDead(_combatData.Player);

        private bool IsUnitDead(IEntity unit) => unit.UnitHealthData.IsDead.Value;
    }
}