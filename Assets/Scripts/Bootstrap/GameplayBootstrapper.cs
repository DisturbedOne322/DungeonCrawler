using Cysharp.Threading.Tasks;
using Gameplay;
using Gameplay.Player;
using Gameplay.Units;
using Zenject;

namespace Bootstrap
{
    public class GameplayBootstrapper : IInitializable
    {
        private readonly GameSequenceController _gameSequenceController;
        private readonly PlayerInitializer _playerInitializer;
        private readonly PlayerUnit _playerUnit;

        public GameplayBootstrapper(GameSequenceController gameSequenceController, PlayerInitializer playerInitializer,
            PlayerUnit playerUnit)
        {
            _gameSequenceController = gameSequenceController;
            _playerInitializer = playerInitializer;
            _playerUnit = playerUnit;
        }

        public void Initialize()
        {
            _playerInitializer.InitializePlayer(_playerUnit);
            _gameSequenceController.StartRun().Forget();
        }
    }
}