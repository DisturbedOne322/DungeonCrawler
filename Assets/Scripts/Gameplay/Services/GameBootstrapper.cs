using System;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Gameplay.Services
{
    public class GameBootstrapper : IInitializable
    {
        private readonly GameSequenceController _gameSequenceController;

        public GameBootstrapper(GameSequenceController gameSequenceController)
        {
            _gameSequenceController = gameSequenceController;
        }
        
        public void Initialize()
        {
            _gameSequenceController.StartRun().Forget();
        }
    }
}