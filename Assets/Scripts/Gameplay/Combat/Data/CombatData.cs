using Gameplay.Units;

namespace Gameplay.Combat.Data
{
    public class CombatData
    {
        public CombatData(PlayerUnit player)
        {
            Player = player;
        }

        public int TurnCount { get; private set; } = -1;

        public PlayerUnit Player { get; }

        public EnemyUnit Enemy { get; private set; }

        public GameUnit ActiveUnit { get; private set; }

        public GameUnit OtherUnit { get; private set; }

        public void ResetCombat(EnemyUnit enemy)
        {
            TurnCount = -1;

            Enemy = enemy;
            OtherUnit = enemy;

            ActiveUnit = Player;
        }

        public void UpdateCurrentTurnUnit()
        {
            TurnCount++;
            ActiveUnit = TurnCount % 2 == 0 ? Player : Enemy;
            OtherUnit = TurnCount % 2 != 0 ? Player : Enemy;
        }
    }
}