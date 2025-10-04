using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Core
{
    public class TextDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        [SerializeField] private FadeOptions _fadeIn;
        [SerializeField, Min(0.01f)] private float _stayDuration;
        [SerializeField] private FadeOptions _fadeOut;
        
        private Sequence _sequence;

        public void ShowText(string text)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            
            _text.text = text;
            ResetAlpha();

            _sequence.Append(_text.DOFade(1, _fadeIn.FadeDuration).SetEase(_fadeIn.Ease));
            _sequence.AppendInterval(_stayDuration);
            _sequence.Append(_text.DOFade(0, _fadeOut.FadeDuration).SetEase(_fadeOut.Ease));
            _sequence.SetLink(gameObject);
        }

        private void ResetAlpha()
        {
            var color = _text.color;
            color.a = 0;
            _text.color = color;
        }
    }

    [Serializable]
    public class FadeOptions
    {
        [Min(0.01f)] public float FadeDuration = 0.5f;
        public Ease Ease = Ease.Linear;
    }
}