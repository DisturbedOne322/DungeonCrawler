using System;
using DG.Tweening;
using UI.InventoryDisplay;
using UI.Menus.Data;
using UnityEngine;
using UniRx;

namespace UI.Navigation
{
    public class PageMenuItemDataMono : MenuItemDataMono
    {
        private const float AnimDuration = 0.2f;
        
        [SerializeField] private BaseDisplayMenuView _pageView;

        private IDisposable _disposable;
        
        protected override void ProcessAdditionalBindings(MenuItemData data)
        {
            _disposable = data.IsHighlighted.Subscribe(HighlightRelatedPage);
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }

        private void HighlightRelatedPage(bool value)
        {
            float targetAlpha = value ? 1f : 0f;
            _pageView.CanvasGroup.DOKill();
            _pageView.CanvasGroup.DOFade(targetAlpha, AnimDuration).
                SetLink(gameObject).
                SetUpdate(true);
        }
    }
}