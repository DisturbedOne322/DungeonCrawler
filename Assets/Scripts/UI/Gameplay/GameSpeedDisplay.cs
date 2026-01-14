using System;
using System.Globalization;
using Data;
using Data.Constants;
using TMPro;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class GameSpeedDisplay : MonoBehaviour
    {
        [SerializeField] private Image _bgImage;
        [SerializeField] private TextMeshProUGUI _speedText;

        [SerializeField, Space] private Color _defaultColor;
        [SerializeField] private Color _spedUpColor;
        
        private IDisposable _disposable;
        
        [Inject]
        private void Construct(GameplayData gameplayData)
        {
            _disposable = gameplayData.IsSpedUp.Subscribe(ToggleDisplay);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        private void ToggleDisplay(bool spedUp)
        {
            UpdateText(spedUp);
            UpdateColor(spedUp);
        }

        private void UpdateText(bool spedUp)
        {
            string displayText = spedUp
                ? GameplayConstants.SpeedUpTimeScale.ToString("F1", CultureInfo.InvariantCulture)
                : "1.0";
            _speedText.SetText($"x{displayText}");
        }

        private void UpdateColor(bool spedUp)
        {
            _bgImage.color = spedUp ? _spedUpColor : _defaultColor;
        }
    }
}