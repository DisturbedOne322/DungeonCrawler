using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Areas;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class DungeonFactory : MonoBehaviour
    {
        [SerializeField] private CorridorRoom _corridorPrefab;
        [SerializeField] private DecisionRoom _decisionPrefab;

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
            
            switch (roomType)
            {
                case RoomType.Corridor:
                    return _containerFactory.Create<CorridorRoom>(_corridorPrefab.gameObject);
                case RoomType.Decision:
                    return _containerFactory.Create<DecisionRoom>(_decisionPrefab.gameObject);
            }
            
            return null;
        }
    }
}