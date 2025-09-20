using Cysharp.Threading.Tasks;
using Gameplay.Player;

namespace Gameplay.Combat
{
    public class CombatSequenceController
    {
        private readonly PlayerUnit _playerUnit;
        private GameUnit _enemyUnit;
        
        private int _turnIndex = 0;
        
        public CombatSequenceController(PlayerUnit playerUnit)
        {
            _playerUnit = playerUnit;
        }

        public async UniTask StartCombat(GameUnit enemyUnit)
        {
            _turnIndex = -1;

            _enemyUnit = enemyUnit;


            while (!IsCombatOver())
            {
                await ProcessTurn();
            }
        }

        private async UniTask ProcessTurn()
        {
            _turnIndex++;
            var currentUnit = GetCurrentTurnUnit();
            var skill = await currentUnit.SkillSelectionProvider.SelectSkillToUse();
            
            skill.DealDamage(10, GetOtherUnit());
        }

        private GameUnit GetCurrentTurnUnit() => _turnIndex % 2 == 0 ? _playerUnit : _enemyUnit;
        private GameUnit GetOtherUnit() => _turnIndex % 2 == 0 ? _enemyUnit : _playerUnit;

        private bool IsCombatOver() => IsUnitDead(_enemyUnit) || IsUnitDead(_playerUnit);

        private bool IsUnitDead(GameUnit unit) => unit.UnitHealthController.UnitHealthData.IsDead.Value;
    }
}