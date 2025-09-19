using System.Collections.Generic;
using Gameplay.Dungeon.Areas;

namespace Gameplay.Dungeon
{
    public class DungeonLayoutProvider
    {
        private readonly List<DungeonArea> _dungeonAreas = new ();
        
        public List<DungeonArea> DungeonAreas => _dungeonAreas;
        
        public void AddRoom(DungeonArea room) => _dungeonAreas.Add(room);
    }
}