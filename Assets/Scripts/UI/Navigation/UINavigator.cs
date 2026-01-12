using System;
using System.Collections.Generic;
using Gameplay.Player;
using UI.Menus.Data;
using UniRx;

namespace UI.Navigation
{
    public abstract class UINavigator : BaseUIInputHandler
    {
        private readonly List<MenuItemData> _menuItemsData = new();
        private readonly PlayerInputProvider _playerInputProvider;

        private int _prevIndex;
        private int _selectedIndex;

        public UINavigator(PlayerInputProvider playerInputProvider) => 
            _playerInputProvider = playerInputProvider;

        public override void OnUISubmit()
        {
            _menuItemsData[_selectedIndex].OnSelected.Invoke();
        }

        public void AddMenuItem(MenuItemDataMono menuItem, Action callback)
        {
            var data = menuItem.CreateData(callback);
            _menuItemsData.Add(data);

            if (_menuItemsData.Count == 1)
                data.IsHighlighted.Value = true;
        }

        public void BindToObservable(IObservable<Unit> until)
        {
            TakeControl();
            until.Subscribe(_ => RemoveControl());
        }

        protected void UpdateSelection(int increment)
        {
            _menuItemsData[_prevIndex].IsHighlighted.Value = false;

            _selectedIndex = WrapIndex(increment);
            _menuItemsData[_selectedIndex].IsHighlighted.Value = true;

            _prevIndex = _selectedIndex;
        }

        private int WrapIndex(int increment)
        {
            var nextIndex = _selectedIndex + increment;

            if (nextIndex < 0)
                nextIndex = _menuItemsData.Count - 1;
            else if (nextIndex >= _menuItemsData.Count)
                nextIndex = 0;

            return nextIndex;
        }
        
        public void TakeControl() => _playerInputProvider.AddUiInputOwner(this);
 
        public void RemoveControl() =>  _playerInputProvider.RemoveUiInputOwner(this);
    }
}