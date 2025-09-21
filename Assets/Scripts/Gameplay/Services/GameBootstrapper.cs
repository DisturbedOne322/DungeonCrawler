using System;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Gameplay.Units;
using Zenject;

namespace Gameplay.Services
{
    public class GameBootstrapper : IInitializable
    {
        private readonly GameSequenceController _gameSequenceController;
        private readonly PlayerFactory _playerFactory;
        private readonly PlayerUnit _playerUnit;

        public GameBootstrapper(GameSequenceController gameSequenceController, PlayerFactory playerFactory, PlayerUnit playerUnit)
        {
            _gameSequenceController = gameSequenceController;
            _playerFactory = playerFactory;
            _playerUnit = playerUnit;
        }
        
        public void Initialize()
        {
            _playerFactory.InitializePlayer(_playerUnit);
            _gameSequenceController.StartRun().Forget();
        }
    }
}