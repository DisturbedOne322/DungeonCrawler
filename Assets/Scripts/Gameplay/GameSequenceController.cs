using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Dungeon;
using Gameplay.Dungeon.RoomVariants;
using Gameplay.Player;
using Gameplay.Rewards;
using Gameplay.Units;
using StateMachine.App;
using UI;
using UI.Gameplay;
using UI.Popups;
using UnityEngine;

namespace Gameplay
{
    public class GameSequenceController
    {
        private readonly DungeonBranchingSelector _dungeonBranchingSelector;
        private readonly DungeonGenerator _dungeonGenerator;
        private readonly PlayerMovementController _playerMovementController;
        private readonly PlayerUnit _playerUnit;
        private readonly RoomDropsService _roomDropsService;
        private readonly AppStateMachine _stateMachine;
        private readonly UIFactory _uiFactory;

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
            _dungeonGenerator.CreateFirstMapSection();
            _playerMovementController.PositionPlayer();

            while (IsPlayerAlive())
            {
                var targetRoom = _playerMovementController.GetNextInteractiveRoom();

                await _playerMovementController.MovePlayer();
                
                await targetRoom.PlayEnterSequence();
                await targetRoom.ClearRoom();

                if (IsPlayerAlive())
                    await _roomDropsService.GiveRewardToPlayer(targetRoom);
            }

            ShowGameOverPopup();
        }

        private bool IsPlayerAlive()
        {
            return !_playerUnit.UnitHealthData.IsDead.Value;
        }

        private void ShowGameOverPopup()
        {
            var popup = _uiFactory.CreatePopup<GameOverPopup>();
            popup.OnExitPressed += () => _stateMachine.GoToState<MainMenuAppState>().Forget();
            popup.OnRetryPressed += () => _stateMachine.GoToState<GameplayAppState>().Forget();
        }
    }
}