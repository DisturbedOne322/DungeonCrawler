using System;
using DG.Tweening;
using Gameplay.Units;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay
{
    public class BalanceDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentBalanceText;
        [SerializeField] private TextMeshProUGUI _newBalanceText;
        [SerializeField] private float _fadeDuration = 0.25f;
        [SerializeField] private float _transferDuration = 1.5f;
        
        private Sequence _sequence;
        
        private IDisposable _disposable;
        
        [Inject]
        private void Construct(PlayerUnit playerUnit)
        {
            _disposable = playerUnit.UnitInventoryData.Coins
                .Pairwise()
                .Subscribe(pair =>
                {
                    var prev = pair.Previous;
                    var current = pair.Current;
                    var added = current - prev;

                    ShowBalanceChange(current, added);
                });
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        private void ShowBalanceChange(int newBalance, int addedBalance)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            int startBalance = newBalance - addedBalance;
            int displayedCurrent = startBalance;
            int displayedAdded = addedBalance;

            // Initialize visuals
            _currentBalanceText.text = startBalance.ToString();
            _newBalanceText.text = $"+{addedBalance}";
            _newBalanceText.alpha = 0;

            // Fade in "+X"
            _sequence.Append(_newBalanceText.DOFade(1f, _fadeDuration));

            // Animate value transfer
            _sequence.Append(
                DOTween.To(() => 0f, t =>
                {
                    // `t` goes from 0 → 1 during the transfer
                    displayedAdded = Mathf.RoundToInt(addedBalance * (1f - t));
                    displayedCurrent = Mathf.RoundToInt(startBalance + addedBalance * t);

                    _newBalanceText.text = $"+{displayedAdded}";
                    _currentBalanceText.text = displayedCurrent.ToString();
                }, 1f, _transferDuration)
            );

            // Fade out "+0"
            _sequence.Append(_newBalanceText.DOFade(0f, _fadeDuration));

            _sequence.OnComplete(() =>
            {
                _newBalanceText.text = string.Empty;
                _currentBalanceText.text = newBalance.ToString();
            });
        }
    }
}