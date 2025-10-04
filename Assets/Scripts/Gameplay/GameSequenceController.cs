using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.Rooms;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Services;
using Gameplay.Units;
using StateMachine.App;
using UI;
using UI.Gameplay;

namespace Gameplay
{
    public class GameSequenceController
    {
        private readonly PlayerMovementController _playerMovementController;
        private readonly DungeonBranchingSelector _dungeonBranchingSelector;
        private readonly DungeonGenerator _dungeonGenerator;
        private readonly RoomDropsService _roomDropsService;
        private readonly PlayerUnit _playerUnit;
        private readonly UIFactory _uiFactory;
        private readonly AppStateMachine _stateMachine;

        public GameSequenceController(PlayerMovementController playerMovementController,
            DungeonBranchingSelector dungeonBranchingSelector,
            DungeonGenerator dungeonGenerator,
            RoomDropsService roomDropsService,
            PlayerUnit playerUnit,
            UIFactory uiFactory,
            AppStateMachine stateMachine)
        {
            _playerMovementController = playerMovementController;
            _dungeonBranchingSelector = dungeonBranchingSelector;
            _dungeonGenerator = dungeonGenerator;
            _roomDropsService = roomDropsService;
            _playerUnit = playerUnit;
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
        }
        
        public async UniTask StartRun()
        {
            _dungeonBranchingSelector.PrepareSelection();
            _dungeonGenerator.CreateNextMapSection(RoomType.Corridor);
            _playerMovementController.PositionPlayer();

            while (IsPlayerAlive())
            {
                var stopRoom = _playerMovementController.GetNextStopRoom();
                
                await _playerMovementController.MovePlayer();
                
                await stopRoom.PlayEnterSequence();
                await stopRoom.ClearRoom();

                if(IsPlayerAlive())
                    await _roomDropsService.GiveRewardToPlayer(stopRoom);
            }
            
            ShowGameOverPopup();
        }

        private bool IsPlayerAlive() => !_playerUnit.UnitHealthData.IsDead.Value;

        private void ShowGameOverPopup()
        {
            var popup = _uiFactory.CreatePopup<GameOverPopup>();
            popup.OnExitPressed += () => _stateMachine.GoToState<MainMenuAppState>().Forget();
            popup.OnRetryPressed += () => _stateMachine.GoToState<GameplayAppState>().Forget();
        }
    }
}