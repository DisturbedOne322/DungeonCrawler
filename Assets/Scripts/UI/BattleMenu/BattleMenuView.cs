using System;
using StateMachine.BattleMenu;
using UnityEngine;
using Zenject;
using UniRx;

namespace UI.BattleMenu
{
    public class BattleMenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform _pagesParent;
        [SerializeField] private BattleMenuItemViewFactory _factory;
        
        private BattleMenuStateMachine _stateMachine;
        
        private CompositeDisposable _disposables;

        private BattleMenuPageView _currentPage;
        
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
            _disposables.Add(_stateMachine.OnStateChanged.Subscribe(CreateMenu));
            _disposables.Add(_stateMachine.SkillSelected.Subscribe(_ => CloseMenu()));
        }

        private void OnDestroy() => _disposables.Dispose();
        
        private void CloseMenu() => DestroyCurrentPage();

        private void CreateMenu(BattleMenuState state)
        {
            DestroyCurrentPage();
            
            var updater = state.MenuItemsUpdater;

            _currentPage = _factory.CreatePage();
            _currentPage.transform.SetParent(_pagesParent, false);
            _currentPage.SetItems(_factory.CreateViewsForData(updater.MenuItems));
        }

        private void DestroyCurrentPage()
        {
            if (_currentPage) 
                Destroy(_currentPage.gameObject);
        }
    }
}