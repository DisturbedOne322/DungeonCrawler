using StateMachine.BattleMenu;
using UnityEngine;
using Zenject;
using UniRx;

namespace UI.BattleMenu
{
    public class BattleMenuView : MonoBehaviour
    {
        [SerializeField] private BattleMenuItemViewFactory _factory;
        
        [SerializeField] private RectTransform _mainContent;
        [SerializeField] private RectTransform _skillsContent;
        
        private BattleMenuStateMachine _stateMachine;

        [Inject]
        private void Construct(BattleMenuStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Awake()
        {
            _stateMachine.OnOpened.Subscribe(_ => OpenMenu()).AddTo(gameObject);
        }

        private void OpenMenu()
        {
            CreateMenu(_mainContent, _stateMachine.CurrentState.Value);
        }

        private void CreateMenu(RectTransform parent, BattleMenuState state)
        {
            var updater = state.MenuItemsUpdater;

            var views = _factory.CreateViewsForData(updater.MenuItems);

            foreach (var view in views) 
                view.transform.SetParent(parent, false);
        }
    }
}