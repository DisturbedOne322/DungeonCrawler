using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Units;

namespace Gameplay.Combat
{
    public class CombatSequenceController
    {
        private readonly CombatData _combatData;
        
        public CombatSequenceController(CombatData combatData)
        {
            _combatData = combatData;
        }

        public async UniTask StartCombat(GameUnit enemyUnit)
        {
            _combatData.StartCombat(enemyUnit);
            
            while (!IsCombatOver())
            {
                _combatData.ProcessTurn();
                await ProcessTurn();
            }
        }

        private async UniTask ProcessTurn()
        {
            var currentUnit = _combatData.ActiveUnit;
            
            var skill = await currentUnit.SkillSelectionProvider.SelectSkillToUse();
            await skill.UseSkill(_combatData);
        }
        
        private bool IsCombatOver() => IsUnitDead(_combatData.Enemy) || IsUnitDead(_combatData.Player);

        private bool IsUnitDead(GameUnit unit) => unit.UnitHealthController.UnitHealthData.IsDead.Value;
    }
}