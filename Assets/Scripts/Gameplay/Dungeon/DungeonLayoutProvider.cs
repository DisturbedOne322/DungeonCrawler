using System.Collections.Generic;
using Gameplay.Dungeon.Rooms;

namespace Gameplay.Dungeon
{
    public class DungeonLayoutProvider
    {
        public List<DungeonRoom> DungeonAreas { get; } = new();

        public int RoomsCount => DungeonAreas.Count;

        public void AddRoom(DungeonRoom room)
        {
            DungeonAreas.Add(room);
        }

        public bool TryGetRoom(int index, out DungeonRoom room)
        {
            if (index < 0 || index >= RoomsCount)
            {
                room = null;
                return false;
            }

            room = DungeonAreas[index];
            return true;
        }
    }
}