using System.Collections.Generic;
using StateMachine.BattleMenu;
using UnityEngine;
using Zenject;
using UniRx;

namespace UI.BattleMenu
{
    public class BattleMenuController : MonoBehaviour
    {
        [SerializeField] private RectTransform _pagesParent;
        [SerializeField] private BattleMenuItemViewFactory _factory;
        
        private BattleMenuStateMachine _stateMachine;
        
        private CompositeDisposable _disposables;

        private BattleMenuPageView _currentPage;
        
        private Dictionary<BattleMenuState, BattleMenuPageView> _pagePool = new();
        
        [Inject]
        private void Construct(BattleMenuStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Awake()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _disposables = new();
            _disposables.Add(_stateMachine.OnStateChanged.Subscribe(OpenMenu));
            _disposables.Add(_stateMachine.ActionSelected.Subscribe(_ => DestroyAllMenus()));
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
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

            _currentPage = _factory.CreatePage();
            _currentPage.transform.SetParent(_pagesParent, false);
            _currentPage.SetItems(_factory.CreateViewsForData(updater.MenuItems));
            
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