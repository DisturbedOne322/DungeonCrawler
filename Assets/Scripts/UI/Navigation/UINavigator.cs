using System;
using System.Collections.Generic;
using Gameplay.Player;
using UI.BattleMenu;
using UniRx;
using UnityEngine;

namespace UI.Navigation
{
    public abstract class UINavigator
    {
        protected readonly PlayerInputProvider PlayerInputProvider;
        protected readonly CompositeDisposable Disposables = new ();

        private readonly List<MenuItemData> _menuItemsData = new();

        private int _prevIndex = 0;
        private int _selectedIndex = 0;
        
        public UINavigator(PlayerInputProvider playerInputProvider)
        {
            PlayerInputProvider = playerInputProvider;
        }

        public void Initialize()
        {
            PlayerInputProvider.OnUiSubmit.Subscribe(_ =>
            {
                _menuItemsData[_selectedIndex].OnSelected.Invoke();
            }).AddTo(Disposables);
            
            SubscribeToPlayerInput();
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }

        public void AddMenuItem(MenuItemDataMono menuItem, Action callback)
        {
            var data = menuItem.CreateData(callback);
            _menuItemsData.Add(data);
            
            if (_menuItemsData.Count == 1)
                data.IsHighlighted.Value = true;
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
            int nextIndex = _selectedIndex + increment;
            
            if(nextIndex < 0)
                nextIndex = _menuItemsData.Count - 1;
            else if (nextIndex >= _menuItemsData.Count)
                nextIndex = 0;

            return nextIndex;
        }

        protected abstract void SubscribeToPlayerInput();
    }
}