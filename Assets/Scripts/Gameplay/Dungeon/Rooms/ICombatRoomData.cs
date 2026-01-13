using System.Collections.Generic;
using Gameplay.Units;

namespace Gameplay.Dungeon.Rooms
{
    public interface ICombatRoomData
    {
        public IReadOnlyList<EnemyUnitData> EnemiesSelection { get; }
    }
}