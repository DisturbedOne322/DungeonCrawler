using System;
using System.Text;
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
        private readonly StringBuilder _builder = new(8);
        
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
                    var delta = current - prev;

                    if (delta != 0)
                        ShowBalanceChange(current, delta);
                    else
                        _currentBalanceText.text = current.ToString();
                });
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }

        private void ShowBalanceChange(int newBalance, int delta)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            int startBalance = newBalance - delta;
            int absDelta = Mathf.Abs(delta);
            bool isGain = delta > 0;

            _currentBalanceText.text = startBalance.ToString();
            _newBalanceText.alpha = 0f;
            UpdateChangeText(isGain, absDelta);

            _sequence.Append(_newBalanceText.DOFade(1f, _fadeDuration));

            _sequence.Append(
                DOTween.To(() => 0f, t =>
                    {
                        int displayedChange = Mathf.RoundToInt(absDelta * (1f - t));
                        int displayedBalance = Mathf.RoundToInt(startBalance + delta * t);

                        UpdateChangeText(isGain, displayedChange);
                        _currentBalanceText.SetText(displayedBalance.ToString());
                    }, 1f, _transferDuration)
                    .SetEase(Ease.OutQuad)
            );

            _sequence.Append(_newBalanceText.DOFade(0f, _fadeDuration));

            _sequence.OnComplete(() => _newBalanceText.text = string.Empty);
        }

        private void UpdateChangeText(bool isGain, int value)
        {
            _builder.Clear();
            _builder.Append(isGain ? '+' : '-');
            _builder.Append(value);
            _newBalanceText.SetText(_builder);
        }
    }
}