using System;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Data;
using Gameplay.Dungeon.Rooms;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class DungeonFactory : MonoBehaviour
    {
        [SerializeField] private DungeonRoomsDatabase _dungeonRoomsDatabase;

        private ContainerFactory _containerFactory;
        private DungeonRoomsPool _roomsPool;
        
        private Transform _parent;

        [Inject]
        private void Construct(ContainerFactory containerFactory, DungeonRoomsPool roomsPool)
        {
            _containerFactory = containerFactory;
            _roomsPool = roomsPool;
            
        }

        private void Awake()
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
            if (_dungeonRoomsDatabase.TryGetRoomData(roomType, out var data))
                return data;
            
            throw new Exception($"No room data found of type {roomType}");
        }
    }
}