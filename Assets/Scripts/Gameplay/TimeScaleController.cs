using Data;
using UnityEngine;

namespace Gameplay
{
    public class TimeScaleController
    {
        private readonly GameplayData _gameplayData;

        private float _gameplayTimescale = 1;

        public TimeScaleController(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;
        }

        public void SetGameplayTimeScale(float targetTimeScale)
        {
            _gameplayTimescale = targetTimeScale;
            UpdateTimeScale(_gameplayTimescale);
        }

        public void Pause()
        {
            UpdateTimeScale(0);
        }

        public void Unpause()
        {
            UpdateTimeScale(_gameplayTimescale);
        }

        private void UpdateTimeScale(float timeScale)
        {
            _gameplayData.TimeScale.Value = timeScale;
            Time.timeScale = timeScale;
        }
    }
}