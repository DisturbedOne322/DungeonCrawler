using Data;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Services;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonGenerator
    {
        private const int MinCorridorLength = 3;
        private const int MaxCorridorLength = 8;

        private readonly DungeonFactory _dungeonFactory;
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;
        private readonly DungeonPositioner _dungeonPositioner;
        private readonly DungeonRoomsProvider _dungeonRoomsProvider;
        private readonly EncounterRoomSelector _encounterRoomSelector;
        private readonly DungeonBranchingSelector _dungeonBranchingSelector;

        private DungeonGenerator(DungeonLayoutProvider dungeonLayoutProvider,
            DungeonFactory dungeonFactory, DungeonPositioner dungeonPositioner,
            DungeonRoomsProvider dungeonRoomsProvider, EncounterRoomSelector encounterRoomSelector,
            DungeonBranchingSelector dungeonBranchingSelector)
        {
            _dungeonLayoutProvider = dungeonLayoutProvider;
            _dungeonFactory = dungeonFactory;
            _dungeonPositioner = dungeonPositioner;
            _dungeonRoomsProvider = dungeonRoomsProvider;
            _encounterRoomSelector = encounterRoomSelector;
            _dungeonBranchingSelector = dungeonBranchingSelector;
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
            var roomsToSpawn = Random.Range(MinCorridorLength, MaxCorridorLength);

            var specialRoom = _encounterRoomSelector.SelectSpecialRoomData(roomsToSpawn);

            for (var i = 0; i < roomsToSpawn; i++)
                if (specialRoom.HasValue && specialRoom.Value.Index == i)
                    CreateRoom(specialRoom.Value.RoomData);
                else
                    CreateCorridor();
        }

        private void CreateDecisionRoom()
        {
            int selectionCount = _dungeonBranchingSelector.RoomsForSelection.Count;
            CreateRoom(_dungeonRoomsProvider.GetDecisionRoomData(selectionCount));
        }

        private void CreateCorridor()
        {
            CreateRoom(_dungeonRoomsProvider.GetRoomData(RoomType.Corridor));
        }

        private void CreateRoom(RoomVariantData roomData)
        {
            var room = _dungeonFactory.CreateRoom(roomData);

            room.SetData(roomData);
            room.SetupRoom();

            _dungeonLayoutProvider.AddRoom(room);
            _dungeonPositioner.PlaceRoom(room);
        }
    }
}