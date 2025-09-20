using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon;
using Gameplay.Player;

namespace Gameplay
{
    public class GameSequenceController
    {
        private readonly PlayerMovementController _playerMovementController;
        private readonly DungeonGenerator _dungeonGenerator;

        public GameSequenceController(PlayerMovementController playerMovementController,
            DungeonGenerator dungeonGenerator)
        {
            _playerMovementController = playerMovementController;
            _dungeonGenerator = dungeonGenerator;
        }
        
        public async UniTask StartRun()
        {
            _dungeonGenerator.CreateNextMapSection(RoomType.Corridor);
            _playerMovementController.PositionPlayer();

            while (true)
            {
                await _playerMovementController.MovePlayer();

                var currentRoom = _playerMovementController.GetCurrentRoom();
                await currentRoom.ClearRoom();
            }
        }
    }
}