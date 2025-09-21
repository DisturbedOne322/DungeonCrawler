using Cysharp.Threading.Tasks;
using Gameplay.Units;

namespace Gameplay.Combat
{
    public class CombatSequenceController
    {
        private readonly CombatData _combatData;
        private readonly CombatService _combatService;
        
        public CombatSequenceController(CombatData combatData, CombatService combatService)
        {
            _combatData = combatData;
            _combatService = combatService;
        }

        public async UniTask StartCombat(GameUnit enemyUnit)
        {
            _combatService.StartCombat(enemyUnit);
            
            while (!IsCombatOver())
            {
                _combatService.ProcessTurn();
                await ProcessTurn();
            }
        }

        private async UniTask ProcessTurn()
        {
            var currentUnit = _combatData.ActiveUnit;
            
            var skill = await currentUnit.SkillSelectionProvider.SelectSkillToUse();
            await skill.UseSkill(_combatService);
        }
        
        private bool IsCombatOver() => IsUnitDead(_combatData.Enemy) || IsUnitDead(_combatData.Player);

        private bool IsUnitDead(GameUnit unit) => unit.UnitHealthController.UnitHealthData.IsDead.Value;
    }
}