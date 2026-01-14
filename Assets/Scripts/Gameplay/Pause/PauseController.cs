using System;
using Data;
using Gameplay.Player;
using PopupControllers.Pause;
using UniRx;
using Zenject;

namespace Gameplay.Pause
{
    public class PauseController : IInitializable, IDisposable
    {
        private readonly GameplayData _gameplayData;
        private readonly PausePopupController _pausePopupController;
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly TimeScaleController _timeScaleController;

        private IDisposable _subscription;

        public PauseController(PlayerInputProvider playerInputProvider,
            GameplayData gameplayData,
            TimeScaleController timeScaleController,
            PausePopupController pausePopupController)
        {
            _playerInputProvider = playerInputProvider;
            _gameplayData = gameplayData;
            _timeScaleController = timeScaleController;
            _pausePopupController = pausePopupController;
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        public void Initialize()
        {
            _subscription = _playerInputProvider.OnPause.Subscribe(_ => TogglePause());
        }

        private void TogglePause()
        {
            ToggleGameState();

            var currentState = _gameplayData.CurrentGameState.Value;

            if (currentState == GameState.Paused)
            {
                _timeScaleController.Pause();
                _pausePopupController.OpenPopup();
            }
            else
            {
                _pausePopupController.ClosePopup();
                _timeScaleController.Unpause();
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