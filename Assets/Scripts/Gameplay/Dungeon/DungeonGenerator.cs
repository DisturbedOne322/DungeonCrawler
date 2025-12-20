using Gameplay.Dungeon.RoomVariants;
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
            CreateDecisionRoom();
        }
        
        public void CreateNextMapSection(RoomVariantData firstRoom)
        {
            CreateRoom(firstRoom);
            CreateCorridors();
            CreateDecisionRoom();
        }

        private void CreateCorridors()
        {
            var corridorsAmount = 4;

            for (var i = 0; i < corridorsAmount; i++)
                CreateCorridor();
        }

        private void CreateDecisionRoom() => CreateRoom(_dungeonRoomsProvider.GetDecisionRoomData());

        private void CreateCorridor() => CreateRoom(_dungeonRoomsProvider.GetCorridorRoomData());

        private void CreateRoom(RoomVariantData roomData)
        {
            var room = _dungeonFactory.CreateArea(roomData);
            
            roomData.ApplyToRoom(room);
            room.SetupRoom();

            _dungeonLayoutProvider.AddRoom(room);
            _dungeonPositioner.PlaceRoom(room);
        }
    }
}