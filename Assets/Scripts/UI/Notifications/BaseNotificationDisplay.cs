using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UI.Core;
using UnityEngine;

namespace UI.Notifications
{
    public abstract class BaseNotificationDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private FadeInOutOptions _fadeInOutOptions;

        private Sequence _sequence;

        public async UniTask Show(CancellationToken token)
        {
            var width = _rectTransform.sizeDelta.x;
            _sequence = DOTween.Sequence();

            var fadeIn = _fadeInOutOptions.FadeIn;
            var fadeOut = _fadeInOutOptions.FadeOut;

            _sequence.Append(_rectTransform.DOAnchorPosX(-width, fadeIn.FadeDuration).SetEase(fadeIn.Ease));
            _sequence.AppendInterval(_fadeInOutOptions.StayTime);
            _sequence.Append(_rectTransform.DOAnchorPosX(0, fadeOut.FadeDuration).SetEase(fadeOut.Ease));
            _sequence.SetLink(gameObject);

            await _sequence.AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(token);
        }
    }
}