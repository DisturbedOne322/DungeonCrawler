using System;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Rooms;
using Gameplay.Units;

namespace Gameplay.Player
{
    public class PlayerMovementController
    {
        private const float MoveTime = 1;
        
        private readonly PlayerUnit _playerUnit;
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;
        private readonly PlayerMovementHistory _playerMovementHistory;
        private readonly GameplayData _gameplayData;
        
        public PlayerMovementController(PlayerUnit playerUnit,
            DungeonLayoutProvider dungeonLayoutProvider,
            PlayerMovementHistory playerMovementHistory,
            GameplayData gameplayData)
        {
            _playerUnit = playerUnit;
            _dungeonLayoutProvider = dungeonLayoutProvider;
            _playerMovementHistory = playerMovementHistory;
            _gameplayData = gameplayData;
        }

        public StopRoom GetNextStopRoom()
        {
            if (_dungeonLayoutProvider.TryGetRoom(GetIndexOfStopArea(), out var room))
                return room as StopRoom;

            return null;
        }

        public void PositionPlayer()
        {
            SetPositionIndex(0);
            
            var firstRoom = _dungeonLayoutProvider.DungeonAreas[GetPositionIndex()];
            _playerUnit.transform.position = firstRoom.PlayerStandPoint.position;
            _playerUnit.transform.forward = firstRoom.PlayerStandPoint.forward;
            
            _playerMovementHistory.AddRoom(RoomType.Corridor);
        }

        public async UniTask MovePlayer()
        {
            int targetIndex = GetIndexOfStopArea();
            int currentIndex = GetPositionIndex();
            
            for (; currentIndex < targetIndex;)
            {
                currentIndex++;
                
                if (!_dungeonLayoutProvider.TryGetRoom(currentIndex, out var room))
                    throw new Exception("Invalid room");
                
                await _playerUnit.MoveTowards(new MovementData()
                {
                    TargetPos = room.PlayerStandPoint.position,
                    MoveTime = MoveTime,
                });
                
                _playerMovementHistory.AddRoom(room.RoomType);
                SetPositionIndex(currentIndex);
            }
        }

        private int GetIndexOfStopArea()
        {
            int startIndex = GetPositionIndex() + 1;
            
            int roomsCount = _dungeonLayoutProvider.RoomsCount;
            for (int i = startIndex; i < roomsCount; i++)
            {
                if(_dungeonLayoutProvider.TryGetRoom(i, out var room) && room is StopRoom)
                    return i;
            }
            
            return roomsCount - 1;
        }

        private void SetPositionIndex(int index) => _gameplayData.PlayerPositionIndex.Value = index;

        private int GetPositionIndex() => _gameplayData.PlayerPositionIndex.Value;
    }
}