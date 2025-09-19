using Data;
using Gameplay.Dungeon.Areas;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonPositioner
    {
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;

        public DungeonPositioner(DungeonLayoutProvider dungeonLayoutProvider)
        {
            _dungeonLayoutProvider = dungeonLayoutProvider;
        }

        public void PlaceRoom(DungeonArea room, int decision = -1)
        {
            int existingRooms = _dungeonLayoutProvider.DungeonAreas.Count;
            float posZ = existingRooms * GameplayConstants.RoomSize.y;
            float posX = 0;

            if (decision != -1)
            {
                decision--;
                posX = decision * GameplayConstants.RoomSize.x;
            }
            
            room.transform.position = new Vector3(posX, 0, posZ);
        }
    }
}