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
        private readonly DungeonRoomsPool _dungeonRoomsPool;

        public GameSequenceController(PlayerMovementController playerMovementController,
            DungeonGenerator dungeonGenerator, DungeonRoomsPool dungeonRoomsPool)
        {
            _playerMovementController = playerMovementController;
            _dungeonGenerator = dungeonGenerator;
            _dungeonRoomsPool = dungeonRoomsPool;
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