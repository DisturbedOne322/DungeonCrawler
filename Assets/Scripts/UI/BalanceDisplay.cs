using System;
using Data;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI
{
    public class BalanceDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        
        [Inject]
        private GameplayData _gameplayData;

        private void Awake()
        {
            _balanceText.text = _gameplayData.Coins.Value.ToString();
            _gameplayData.Coins.Subscribe(coins => _balanceText.text = coins.ToString()).AddTo(gameObject);
        }
    }
}