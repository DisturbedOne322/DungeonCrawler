using Data;
using Gameplay.Services;

namespace Gameplay.Dungeon
{
    public class DungeonGenerator
    {
        private readonly DungeonFactory _dungeonFactory;
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;
        private readonly DungeonPositioner _dungeonPositioner;

        private DungeonGenerator(DungeonLayoutProvider dungeonLayoutProvider,
            DungeonFactory dungeonFactory, DungeonPositioner dungeonPositioner)
        {
            _dungeonLayoutProvider = dungeonLayoutProvider;
            _dungeonFactory = dungeonFactory;
            _dungeonPositioner = dungeonPositioner;
        }

        public void CreateNextMapSection(RoomType firstRoom)
        {
            CreateRoom(firstRoom);
            CreateCorridors();
            CreateRoom(RoomType.Decision);
        }

        private void CreateCorridors()
        {
            var corridorsAmount = 4;

            for (var i = 0; i < corridorsAmount; i++)
                CreateRoom(RoomType.Corridor);
        }

        private void CreateRoom(RoomType roomType)
        {
            var room = _dungeonFactory.CreateArea(roomType);
            room.SetupRoom();

            _dungeonLayoutProvider.AddRoom(room);
            _dungeonPositioner.PlaceRoom(room);
        }
    }
}