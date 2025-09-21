using Gameplay.Player;
using Gameplay.Units;

namespace Gameplay.Combat
{
    public class CombatData
    {
        private GameUnit _player;
        private GameUnit _enemy;

        private GameUnit _activeUnit;
        private GameUnit _otherUnit;
        
        public GameUnit Player => _player;
        public GameUnit Enemy => _enemy;
        
        public GameUnit ActiveUnit => _activeUnit;
        public GameUnit OtherUnit => _otherUnit;
        
        public CombatData(PlayerUnit player)
        {
            _player = player;
        }

        public void ResetCombat(GameUnit enemy)
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