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
        private readonly RoomDropsService _roomDropsService;

        public GameSequenceController(PlayerMovementController playerMovementController,
            DungeonBranchingSelector dungeonBranchingSelector,
            DungeonGenerator dungeonGenerator,
            RoomDropsService roomDropsService)
        {
            _playerMovementController = playerMovementController;
            _dungeonBranchingSelector = dungeonBranchingSelector;
            _dungeonGenerator = dungeonGenerator;
            _roomDropsService = roomDropsService;
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

                await _roomDropsService.GiveRewardToPlayer(stopRoom);
            }
        }
    }
}