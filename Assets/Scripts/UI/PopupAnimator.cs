using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class PopupAnimator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;

        [SerializeField] private float _showTime = 0.5f;
        [SerializeField] private float _hideTime = 0.5f;

        public void PlayShowAnim()
        {
            _canvas.alpha = 0;
            _canvas.DOFade(1, _showTime).SetEase(Ease.OutBack).SetLink(gameObject);
        }

        public void PlayHideAnim(Action callback = null)
        {
            _canvas.DOKill();
            _canvas.DOFade(0, _hideTime).
                SetEase(Ease.OutBack).
                SetLink(gameObject).
                OnComplete(() => callback?.Invoke());
        }
    }
}