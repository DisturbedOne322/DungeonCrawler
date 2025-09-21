using System.Collections.Generic;
using Gameplay.Player;
using UniRx;

namespace UI.PlayerBattleMenu
{
    public abstract class BaseMenuState
    {
        protected readonly PlayerInputProvider Input;
        protected readonly BattleMenuController Controller;
        protected readonly MenuNavigator Navigator = new();

        protected List<MenuItemView> Items = new();
        private CompositeDisposable _disposables;

        protected BaseMenuState(PlayerInputProvider input, BattleMenuController controller)
        {
            Input = input;
            Controller = controller;
        }

        public void Initialize(List<MenuItemView> items) => Items = items;

        public virtual void EnterState()
        {
            _disposables = new CompositeDisposable();
            SubscribeInput(_disposables);
            Navigator.ResetSelection(Items);
        }

        public virtual void ExitState()
        {
            _disposables?.Dispose();
        }

        protected abstract void SubscribeInput(CompositeDisposable disposables);
    }
}