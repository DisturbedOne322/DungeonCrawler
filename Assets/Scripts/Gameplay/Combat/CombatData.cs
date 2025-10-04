using Gameplay.Player;
using Gameplay.Units;

namespace Gameplay.Combat
{
    public class CombatData
    {
        private PlayerUnit _player;
        private EnemyUnit _enemy;

        private GameUnit _activeUnit;
        private GameUnit _otherUnit;
        
        public PlayerUnit Player => _player;
        public EnemyUnit Enemy => _enemy;
        
        public GameUnit ActiveUnit => _activeUnit;
        public GameUnit OtherUnit => _otherUnit;
        
        public CombatData(PlayerUnit player)
        {
            _player = player;
        }

        public void ResetCombat(EnemyUnit enemy)
        {
            _enemy = enemy;
            _activeUnit = _player;
        }
        
        public void UpdateCurrentTurnUnit(int turnCount)
        {
            _activeUnit = turnCount % 2 == 0 ? _player : _enemy;
            _otherUnit = turnCount % 2 != 0 ? _player : _enemy;
        }
    }
}