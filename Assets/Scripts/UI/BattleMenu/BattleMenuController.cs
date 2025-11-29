using System.Collections.Generic;
using StateMachine.BattleMenu;
using UI.Menus;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.BattleMenu
{
    public class BattleMenuController : MonoBehaviour
    {
        [SerializeField] private RectTransform _pagesParent;

        private readonly Dictionary<BattleMenuState, MenuPageView> _pagePool = new();

        private MenuPageView _currentPage;

        private CompositeDisposable _disposables;
        private MenuItemViewFactory _menuFactory;

        private BattleMenuStateMachine _stateMachine;

        private void Awake()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        [Inject]
        private void Construct(BattleMenuStateMachine stateMachine, MenuItemViewFactory menuFactory)
        {
            _stateMachine = stateMachine;
            _menuFactory = menuFactory;
        }

        private void Subscribe()
        {
            _disposables = new CompositeDisposable();
            _disposables.Add(_stateMachine.OnStateChanged.Subscribe(OpenMenu));
            _disposables.Add(_stateMachine.ActionSelected.Subscribe(_ => DestroyAllMenus()));
        }

        private void OpenMenu(BattleMenuState state)
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

        private void CreateMenu(BattleMenuState state)
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

        private void DestroyAllMenus()
        {
            foreach (var view in _pagePool.Values)
                Destroy(view.gameObject);

            _currentPage = null;
            _pagePool.Clear();
        }
    }
}