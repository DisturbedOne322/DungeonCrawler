using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Rooms;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;

namespace Gameplay
{
    public class GameSequenceController
    {
        private readonly PlayerMovementController _playerMovementController;
        private readonly DungeonBranchingSelector _dungeonBranchingSelector;
        private readonly DungeonGenerator _dungeonGenerator;
        private readonly DungeonFactory _dungeonFactory;
        private readonly RewardSelectorService _rewardSelectorService;

        public GameSequenceController(PlayerMovementController playerMovementController,
            DungeonBranchingSelector dungeonBranchingSelector,
            DungeonGenerator dungeonGenerator,
            DungeonFactory dungeonFactory,
            RewardSelectorService rewardSelectorService)
        {
            _playerMovementController = playerMovementController;
            _dungeonBranchingSelector = dungeonBranchingSelector;
            _dungeonGenerator = dungeonGenerator;
            _dungeonFactory = dungeonFactory;
            _rewardSelectorService = rewardSelectorService;
        }
        
        public async UniTask StartRun()
        {
            _dungeonBranchingSelector.PrepareSelection();
            _dungeonGenerator.CreateNextMapSection(RoomType.Corridor);
            _playerMovementController.PositionPlayer();

            while (true)
            {
                var stopRoom = _playerMovementController.GetNextStopRoom();
                
                await _playerMovementController.MovePlayer();
                
                await stopRoom.PlayEnterSequence();
                await stopRoom.ClearRoom();

                TryAddRewardsFrom(stopRoom);
            }
        }

        private void TryAddRewardsFrom(DungeonRoom room)
        {
            var roomType = room.RoomType;
            var roomData = _dungeonFactory.GetRoomData(roomType);
            
            _rewardSelectorService.TryAddReward(roomData.RewardDropTable);
        }
    }
}