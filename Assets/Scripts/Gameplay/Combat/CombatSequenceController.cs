using Cysharp.Threading.Tasks;
using Gameplay.Experience;
using Gameplay.Facades;
using Gameplay.Units;

namespace Gameplay.Combat
{
    public class CombatSequenceController
    {
        private readonly CombatData _combatData;
        private readonly CombatService _combatService;
        private readonly PlayerExperienceService _playerExperienceService;
        
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
            
            while (!IsCombatOver())
            {
                _combatService.StartTurn();
                await ProcessTurn();
                _combatService.EndTurn();
            }

            _combatService.EndCombat();
            
            if(IsUnitDead(enemyUnit))
                await _playerExperienceService.AddExperience(enemyUnit.ExperienceBonus);
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