using System;
using AssetManagement.AssetProviders.Core;
using Data;
using Gameplay.Configs;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Data;
using Gameplay.Dungeon.Rooms;
using Gameplay.Dungeon.RoomTypes;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class DungeonFactory : IInitializable
    {
        private readonly BaseConfigProvider<GameplayConfig> _configProvider;
        private readonly ContainerFactory _containerFactory;
        private readonly DungeonRoomsPool _roomsPool;

        private Transform _parent;

        private DungeonFactory(ContainerFactory containerFactory, DungeonRoomsPool roomsPool,
            BaseConfigProvider<GameplayConfig> configProvider)
        {
            _containerFactory = containerFactory;
            _roomsPool = roomsPool;
            _configProvider = configProvider;
        }

        public void Initialize()
        {
            _parent = new GameObject("[ROOMS POOL]").transform;
        }

        public DungeonRoom CreateArea(RoomType roomType)
        {
            if (_roomsPool.TryGetRoom(roomType, out var room))
                return room;

            var roomData = GetRoomData(roomType);
            var newRoom = _containerFactory.Create<DungeonRoom>(roomData.RoomPrefab.gameObject);

            newRoom.transform.SetParent(_parent);

            return newRoom;
        }

        public RoomData GetRoomData(RoomType roomType)
        {
            var config = _configProvider.GetConfig<DungeonRoomsDatabase>();
            if (config.TryGetRoomData(roomType, out var data))
                return data;

            throw new Exception($"No room data found of type {roomType}");
        }
    }
}