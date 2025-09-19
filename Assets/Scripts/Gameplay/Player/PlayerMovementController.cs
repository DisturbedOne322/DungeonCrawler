using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Areas;

namespace Gameplay.Player
{
    public class PlayerMovementController
    {
        private const float MoveTime = 1;
        private const float RotateTime = 1;
        
        private readonly PlayerController _playerController;
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;
        private readonly PlayerMovementHistory _playerMovementHistory;
        private readonly GameplayData _gameplayData;
        
        public PlayerMovementController(PlayerController playerController,
            DungeonLayoutProvider dungeonLayoutProvider,
            PlayerMovementHistory playerMovementHistory,
            GameplayData gameplayData)
        {
            _playerController = playerController;
            _dungeonLayoutProvider = dungeonLayoutProvider;
            _playerMovementHistory = playerMovementHistory;
            _gameplayData = gameplayData;
        }

        public StopRoom GetCurrentRoom()
        {
            if (_dungeonLayoutProvider.TryGetRoom(GetPositionIndex(), out var room))
                return room as StopRoom;

            return null;
        }

        public void PositionPlayer()
        {
            SetPositionIndex(0);
            
            var firstRoom = _dungeonLayoutProvider.DungeonAreas[GetPositionIndex()];
            _playerController.transform.position = firstRoom.PlayerStandPoint.position;
            _playerController.transform.forward = firstRoom.PlayerStandPoint.forward;
            
            _playerMovementHistory.AddRoom(RoomType.Corridor);
        }

        public async UniTask MovePlayer()
        {
            int targetIndex = GetIndexOfStopArea();

            for (; GetPositionIndex() < targetIndex;)
            {
                if (!_dungeonLayoutProvider.TryGetRoom(GetPositionIndex(), out var room))
                    throw new Exception("Invalid room");
                
                var position = room.PlayerStandPoint.position;
                await _playerController.MoveTowards(position, MoveTime, RotateTime);
                
                _playerMovementHistory.AddRoom(room.RoomType);
                SetPositionIndex(GetPositionIndex() + 1);
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