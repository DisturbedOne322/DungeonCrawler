using Data.Constants;
using Gameplay.Dungeon.Rooms;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonPositioner
    {
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;

        private float _xOffset;

        public DungeonPositioner(DungeonLayoutProvider dungeonLayoutProvider)
        {
            _dungeonLayoutProvider = dungeonLayoutProvider;
        }

        public void PlaceRoom(DungeonRoom room)
        {
            var roomsCount = _dungeonLayoutProvider.RoomsCount;

            var posZ = roomsCount * GameplayConstants.RoomSize.y;

            room.transform.position = new Vector3(_xOffset, 0, posZ);
        }

        public void AddXOffsetFromChoice(int choiceIndex, int selectionCount)
        {
            switch (selectionCount)
            {
                case 1:
                    break;
                case 2:
                    if (choiceIndex == 0)
                        choiceIndex = -1;
                    
                    _xOffset += choiceIndex * (GameplayConstants.RoomSize.x / 2);
                    break;
                case 3:
                    choiceIndex--;
                    _xOffset += choiceIndex * GameplayConstants.RoomSize.x;
                    break;
            }
        }
    }
}