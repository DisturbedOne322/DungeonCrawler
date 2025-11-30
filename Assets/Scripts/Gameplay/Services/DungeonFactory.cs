using System;
using AssetManagement.AssetProviders;
using AssetManagement.AssetProviders.ConfigProviders;
using AssetManagement.AssetProviders.Core;
using Data;
using Gameplay.Configs;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Data;
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

        private DungeonFactory(ContainerFactory containerFactory, DungeonRoomsPool roomsPool)
        {
            _containerFactory = containerFactory;
            _roomsPool = roomsPool;
        }

        public void Initialize()
        {
            _parent = new GameObject("[ROOMS POOL]").transform;
        }

        public DungeonRoom CreateArea(RoomVariantData roomData)
        {
            if (_roomsPool.TryGetRoom(roomData.RoomType, out var room))
                return room;

            var roomPrefab = roomData.Prefab;
            var newRoom = _containerFactory.Create(roomPrefab);

            newRoom.transform.SetParent(_parent);

            return newRoom.GetComponent<DungeonRoom>();
        }
    }
}