using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class PopupAnimator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;

        [SerializeField] private float _showTime = 0.5f;
        [SerializeField] private float _hideTime = 0.5f;

        public async UniTask PlayShowAnim()
        {
            _canvas.alpha = 0;
            await _canvas.DOFade(1, _showTime).SetEase(Ease.OutBack).SetLink(gameObject).AsyncWaitForCompletion()
                .AsUniTask();
        }

        public async UniTask PlayHideAnim(Action callback = null)
        {
            _canvas.DOKill();
            await _canvas.DOFade(0, _hideTime).SetEase(Ease.OutBack).SetLink(gameObject)
                .OnComplete(() => callback?.Invoke()).AsyncWaitForCompletion().AsUniTask();
        }
    }
}