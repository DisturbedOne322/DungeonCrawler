using System;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Combat;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Rooms;
using Gameplay.Units;

namespace Gameplay.Player
{
    public class PlayerMovementController
    {
        private const float MoveTimePerMeter = 0.01f;
        private readonly DungeonLayoutProvider _dungeonLayoutProvider;
        private readonly GameplayData _gameplayData;
        private readonly UnitRegenerationService _unitRegenerationService;

        private readonly PlayerMoveAnimator _moveAnimator;
        private readonly PlayerMovementHistory _playerMovementHistory;

        public PlayerMovementController(PlayerUnit playerUnit,
            DungeonLayoutProvider dungeonLayoutProvider,
            PlayerMovementHistory playerMovementHistory,
            GameplayData gameplayData,
            UnitRegenerationService unitRegenerationService)
        {
            _moveAnimator = playerUnit.PlayerMoveAnimator;
            _dungeonLayoutProvider = dungeonLayoutProvider;
            _playerMovementHistory = playerMovementHistory;
            _gameplayData = gameplayData;
            _unitRegenerationService = unitRegenerationService;
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
            _moveAnimator.transform.position = firstRoom.PlayerStandPoint.position;
            _moveAnimator.transform.forward = firstRoom.PlayerStandPoint.forward;

            _playerMovementHistory.AddRoom(RoomType.Corridor);
        }

        public async UniTask MovePlayer()
        {
            var targetIndex = GetIndexOfStopArea();
            var currentIndex = GetPositionIndex();

            _moveAnimator.CreateSequence();

            for (; currentIndex < targetIndex;)
            {
                currentIndex++;

                if (!_dungeonLayoutProvider.TryGetRoom(currentIndex, out var room))
                    throw new Exception("Invalid room");

                var isLast = currentIndex == targetIndex;

                var moveData = new MovementData
                {
                    TargetPos = room.PlayerStandPoint.position,
                    MoveTimePerMeter = MoveTimePerMeter
                };

                var index = currentIndex;

                _moveAnimator.AppendSequence(moveData, () =>
                {
                    _playerMovementHistory.AddRoom(room.RoomType);
                    SetPositionIndex(index);
                    _unitRegenerationService.RegeneratePlayerOutOfBattle();
                }, isLast);
            }

            await _moveAnimator.ExecuteSequence();
        }

        private int GetIndexOfStopArea()
        {
            var startIndex = GetPositionIndex() + 1;

            var roomsCount = _dungeonLayoutProvider.RoomsCount;
            for (var i = startIndex; i < roomsCount; i++)
                if (_dungeonLayoutProvider.TryGetRoom(i, out var room) && room is StopRoom)
                    return i;

            return roomsCount - 1;
        }

        private void SetPositionIndex(int index)
        {
            _gameplayData.PlayerPositionIndex.Value = index;
        }

        private int GetPositionIndex()
        {
            return _gameplayData.PlayerPositionIndex.Value;
        }
    }
}