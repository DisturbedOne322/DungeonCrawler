using Gameplay.Dungeon.RoomVariants;
using Gameplay.Services;
using UnityEngine;

namespace Gameplay.Dungeon
{
    public class DungeonGenerator
    {
        private const int MinCorridorLength = 3;
        private const int MaxCorridorLength = 8;
        private const int MinTrapIndex = 1;
        private const int TrapMaxIndexOffset = -1;
        
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
            var trapRoomDate = _dungeonRoomsProvider.GetTrapRoomData();
            bool spawnTrap = Random.value <= trapRoomDate.TrapChance;
            
            int roomsToSpawn = Random.Range(MinCorridorLength, MaxCorridorLength);
            
            int trapIndex = Random.Range(MinTrapIndex, roomsToSpawn + TrapMaxIndexOffset);
            
            for (var i = 0; i < roomsToSpawn; i++)
            {
                if (trapIndex == i && spawnTrap)
                    CreateTrapRoom();
                else
                    CreateCorridor();
            }
        }

        private void CreateDecisionRoom() => CreateRoom(_dungeonRoomsProvider.GetDecisionRoomData());

        private void CreateCorridor() => CreateRoom(_dungeonRoomsProvider.GetCorridorRoomData());

        private void CreateTrapRoom() => CreateRoom(_dungeonRoomsProvider.GetTrapRoomData());

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