using System;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Areas;
using Gameplay.Dungeon.Data;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class DungeonFactory : MonoBehaviour
    {
        [SerializeField] private DungeonRoomsDatabase _dungeonRoomsDatabase;

        private ContainerFactory _containerFactory;
        private DungeonRoomsPool _roomsPool;

        [Inject]
        private void Construct(ContainerFactory containerFactory, DungeonRoomsPool roomsPool)
        {
            _containerFactory = containerFactory;
            _roomsPool = roomsPool;
        }
        
        public DungeonRoom CreateArea(RoomType roomType)
        {
            if (_roomsPool.TryGetRoom(roomType, out var room))
                return room;

            if (_dungeonRoomsDatabase.TryGetRoomData(roomType, out var data))
                return _containerFactory.Create<DungeonRoom>(data.RoomPrefab.gameObject);
            
            throw new Exception($"No room found of type {roomType}");
        }
    }
}