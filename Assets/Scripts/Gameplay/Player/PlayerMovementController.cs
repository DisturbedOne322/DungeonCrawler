using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Areas;
using UnityEngine;

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

        public StopArea GetCurrentRoom() => _dungeonLayoutProvider.DungeonAreas[GetPositionIndex()] as StopArea;

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

            var areas = _dungeonLayoutProvider.DungeonAreas;
            for (; GetPositionIndex() < targetIndex;)
            {
                var room = areas[GetPositionIndex()];
                
                var position = room.PlayerStandPoint.position;
                await _playerController.MoveTowards(position, MoveTime, RotateTime);
                
                _playerMovementHistory.AddRoom(room.RoomType);
                SetPositionIndex(GetPositionIndex() + 1);
            }
        }

        private int GetIndexOfStopArea()
        {
            List<DungeonArea> dungeonAreas = _dungeonLayoutProvider.DungeonAreas;
                
            int startIndex = GetPositionIndex() + 1;
            
            for (int i = startIndex; i < dungeonAreas.Count; i++)
            {
                if(dungeonAreas[i] is StopArea)
                    return i;
            }
            
            return dungeonAreas.Count - 1;
        }

        private void SetPositionIndex(int index)
        {
            Debug.Log("POS: " + index);
            Debug.Log("SIZE: " + _dungeonLayoutProvider.DungeonAreas.Count);
            _gameplayData.PlayerPositionIndex.Value = index;
        }

        private int GetPositionIndex() => _gameplayData.PlayerPositionIndex.Value;
    }
}