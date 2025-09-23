using System;
using System.Collections.Generic;
using Gameplay.Dungeon.Rooms;

namespace Gameplay.Dungeon
{
    public class DungeonLayoutProvider
    {
        private readonly List<DungeonRoom> _dungeonAreas = new ();
        
        public List<DungeonRoom> DungeonAreas => _dungeonAreas;
        
        public int RoomsCount => _dungeonAreas.Count;
        
        public void AddRoom(DungeonRoom room) => _dungeonAreas.Add(room);

        public bool TryGetRoom(int index, out DungeonRoom room)
        {
            if (index < 0 || index >= RoomsCount)
            {
                room = null;
                return false;
            }
            
            room = _dungeonAreas[index];
            return true;
        }
    }
}