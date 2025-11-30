using Data;
using Gameplay.Dungeon.Data;
using Gameplay.Services;

namespace Gameplay.Dungeon
{
    public class DungeonGenerator
    {
        private readonly DungeonFactory _dungeonFactory;
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;
        private readonly DungeonPositioner _dungeonPositioner;
        private readonly DungeonRoomsProvider _dungeonRoomsProvider;

        private DungeonGenerator(DungeonLayoutProvider dungeonLayoutProvider,
            DungeonFactory dungeonFactory, DungeonPositioner dungeonPositioner,
            DungeonRoomsProvider dungeonRoomsProvider)
        {
            _dungeonLayoutProvider = dungeonLayoutProvider;
            _dungeonFactory = dungeonFactory;
            _dungeonPositioner = dungeonPositioner;
            _dungeonRoomsProvider = dungeonRoomsProvider;
        }

        public void CreateFirstMapSection()
        {
            CreateCorridors();
            CreateRoom(GetRoomData(RoomType.Decision));
        }
        
        public void CreateNextMapSection(RoomVariantData firstRoom)
        {
            CreateRoom(firstRoom);
            CreateCorridors();
            CreateRoom(GetRoomData(RoomType.Decision));
        }

        private void CreateCorridors()
        {
            var corridorsAmount = 4;

            for (var i = 0; i < corridorsAmount; i++)
                CreateRoom(GetRoomData(RoomType.Corridor));
        }

        private void CreateRoom(RoomVariantData roomData)
        {
            var room = _dungeonFactory.CreateArea(roomData);
            
            roomData.ApplyToRoom(room);
            room.SetupRoom();

            _dungeonLayoutProvider.AddRoom(room);
            _dungeonPositioner.PlaceRoom(room);
        }

        private RoomVariantData GetRoomData(RoomType roomType) => _dungeonRoomsProvider.GetRoomData(roomType);
    }
}