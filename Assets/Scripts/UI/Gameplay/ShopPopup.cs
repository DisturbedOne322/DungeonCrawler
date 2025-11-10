using System.Collections.Generic;
using StateMachine.Shop;
using UI.BattleMenu;
using UI.Core;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Gameplay
{
    public class ShopPopup : BasePopup
    {
        [SerializeField] private RectTransform _pagesParent;

        private readonly Dictionary<BaseShopState, MenuPageView> _pagePool = new();

        private MenuPageView _currentPage;

        private CompositeDisposable _disposables;
        private MenuItemViewFactory _menuFactory;

        private ShopStateMachine _stateMachine;

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        [Inject]
        private void Construct(MenuItemViewFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }

        public void Initialize(ShopStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            Subscribe();
        }

        private void Subscribe()
        {
            _disposables = new CompositeDisposable();
            _disposables.Add(_stateMachine.OnStateChanged.Subscribe(OpenMenu));
        }

        private void OpenMenu(BaseShopState state)
        {
            CloseCurrentMenu();

            if (_pagePool.TryGetValue(state, out var page))
            {
                _currentPage = page;
                _currentPage.gameObject.SetActive(true);
                return;
            }

            CreateMenu(state);
        }

        private void CreateMenu(BaseShopState state)
        {
            var updater = state.MenuItemsUpdater;

            _currentPage = _menuFactory.CreatePage();
            _currentPage.transform.SetParent(_pagesParent, false);
            _currentPage.SetItems(_menuFactory.CreateViewsForData(updater.MenuItems), updater);

            _pagePool.TryAdd(state, _currentPage);
        }

        private void CloseCurrentMenu()
        {
            if (_currentPage)
                _currentPage.gameObject.SetActive(false);
        }
    }
}