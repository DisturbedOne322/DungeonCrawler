using System;
using Cysharp.Threading.Tasks;
using Data;
using Gameplay.Player;
using UI;
using UI.Popups;
using UniRx;
using Zenject;

namespace Gameplay.Pause
{
    public class PauseController : IInitializable, IDisposable
    {
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly GameplayData _gameplayData;
        private readonly TimeScaleController _timeScaleController;
        private readonly UIFactory _uiFactory;

        private IDisposable _subscription;
        
        private PausePopup _pausePopup;
        
        public PauseController(PlayerInputProvider playerInputProvider,
            GameplayData gameplayData,
            TimeScaleController timeScaleController,
            UIFactory uiFactory)
        {
            _playerInputProvider = playerInputProvider;
            _gameplayData = gameplayData;
            _timeScaleController = timeScaleController;
            _uiFactory = uiFactory;
        }

        public void Initialize()
        {
            _subscription = _playerInputProvider.OnPause.Subscribe(_ => TogglePause());
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        private void TogglePause()
        {
            ToggleGameState();

            var currentState = _gameplayData.CurrentGameState.Value;

            if (currentState == GameState.Paused)
            {
                _timeScaleController.Pause();
                _pausePopup = _uiFactory.CreatePopup<PausePopup>();
            }
            else
            {
                _timeScaleController.Unpause();
                _pausePopup?.HidePopup();
            }
        }

        private void ToggleGameState()
        {
            var currentState = _gameplayData.CurrentGameState.Value;
            var newState = currentState == GameState.Playing ? GameState.Paused : GameState.Playing;
            _gameplayData.CurrentGameState.Value = newState;
        }
    }
}