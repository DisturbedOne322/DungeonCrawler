using System;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class GameOverPopup : BasePopup
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _retryButton;

        public event Action OnExitPressed;
        public event Action OnRetryPressed;

        protected override void InitializePopup()
        {
            _exitButton.onClick.AddListener(() => OnExitPressed?.Invoke());
            _retryButton.onClick.AddListener(() => OnRetryPressed?.Invoke());
        }
    }
}