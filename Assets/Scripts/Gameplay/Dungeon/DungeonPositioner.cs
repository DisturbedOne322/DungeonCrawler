using Constants;
using Data;
using Gameplay.Dungeon.Rooms;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonPositioner
    {
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;

        private float _xOffset = 0;

        public DungeonPositioner(DungeonLayoutProvider dungeonLayoutProvider)
        {
            _dungeonLayoutProvider = dungeonLayoutProvider;
        }

        public void PlaceRoom(DungeonRoom room)
        {
            int roomsCount = _dungeonLayoutProvider.RoomsCount;
            
            float posZ = roomsCount * GameplayConstants.RoomSize.y;
            
            room.transform.position = new Vector3(_xOffset, 0, posZ);
        }

        public void AddXOffsetFromChoice(int choiceIndex)
        {
            choiceIndex --;
            _xOffset += choiceIndex * GameplayConstants.RoomSize.x;
        }
    }
}