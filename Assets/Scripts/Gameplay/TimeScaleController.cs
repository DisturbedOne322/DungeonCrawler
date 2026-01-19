using System;
using Data;
using Data.Constants;
using Gameplay.Pause;
using Gameplay.Player;
using UnityEngine;
using UniRx;

namespace Gameplay
{
    public class TimeScaleController : IGameplayService
    {
        private readonly GameplayData _gameplayData;

        private IDisposable _subscription;
        
        private float _gameplayTimescale = 1;

        public TimeScaleController(GameplayData gameplayData, PlayerInputProvider playerInputProvider)
        {
            _gameplayData = gameplayData;
            _subscription = playerInputProvider.OnSpeedUp.Subscribe(_ => ToggleSpeedUp());
        }

        public void ProcessGameplayStart()
        {
            UpdateTimeScale(1);
        }

        public void ProcessGameplayEnd()
        {
            _subscription.Dispose();
            UpdateTimeScale(1);
        }
        
        public void Pause()
        {
            UpdateTimeScale(0);
        }

        public void Unpause()
        {
            UpdateTimeScale(_gameplayTimescale);
        }

        private void ToggleSpeedUp()
        {
            bool spedUp = !_gameplayData.IsSpedUp.Value;

            _gameplayData.IsSpedUp.Value = spedUp;
            _gameplayTimescale = spedUp ? GameplayConstants.SpeedUpTimeScale : 1;
            
            if(_gameplayData.CurrentGameState.Value == GameState.Paused)
                return;
            
            UpdateTimeScale(_gameplayTimescale);
        }

        private void UpdateTimeScale(float timeScale)
        {
            _gameplayData.TimeScale.Value = timeScale;
            Time.timeScale = timeScale;
        }
    }
}