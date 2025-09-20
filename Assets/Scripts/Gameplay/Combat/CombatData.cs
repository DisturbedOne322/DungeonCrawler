using Gameplay.Player;

namespace Gameplay.Combat
{
    public class CombatData
    {
        private GameUnit _player;
        private GameUnit _enemy;

        private GameUnit _activeUnit;
        private GameUnit _otherUnit;
        
        private int _turnCount;
        
        public GameUnit Player => _player;
        public GameUnit Enemy => _enemy;
        
        public GameUnit ActiveUnit => _activeUnit;
        public GameUnit OtherUnit => _otherUnit;
        
        public int TurnCount => _turnCount;

        public CombatData(PlayerUnit player)
        {
            _player = player;
        }

        public void StartCombat(GameUnit enemy)
        {
            _enemy = enemy;

            _activeUnit = _player;
            _turnCount = -1;
        }
        
        public void ProcessTurn()
        {
            _turnCount++;
            UpdateCurrentTurnUnit();
        }
        
        private void UpdateCurrentTurnUnit()
        {
            _activeUnit = _turnCount % 2 == 0 ? _player : _enemy;
            _otherUnit = _turnCount % 2 != 0 ? _player : _enemy;
        }
    }
}