using System.Collections.Generic;

namespace StateMachine.BattleMenu
{
    public class MenuItemsUpdater
    {
        private List<BattleMenuItemData> _menuItems;
        public List<BattleMenuItemData> MenuItems => _menuItems;

        private int _selectedIndex = 0;
        
        public void SetMenuItems(List<BattleMenuItemData> menuItems) => _menuItems = menuItems;

        public void UpdateSelection(int increment)
        {
            int count = _menuItems.Count;
            
            if (count == 0) 
                return;

            _selectedIndex = FindNextSelectableIndex(increment);
            if (_selectedIndex == -1)
                return;

            ApplyHighlight();
        }

        public void ResetSelection(bool rememberSelection = true)
        {
            if(_menuItems == null)
                return;
            
            if (!rememberSelection || !IsSelectionValid()) 
                _selectedIndex = FindFirstSelectableIndex();

            ApplyHighlight();
        }

        public void ExecuteSelection()
        {
            if (!IsSelectionValid()) 
                return;
            
            _menuItems[_selectedIndex].OnSelected?.Invoke();
        }
        
        private void ApplyHighlight()
        {
            for (int i = 0; i < _menuItems.Count; i++)
            {
                var item = _menuItems[i];
                item.IsHighlighted.Value = (i == _selectedIndex && item.IsSelectable());
            }
        }

        private int FindFirstSelectableIndex()
        {
            int count = _menuItems.Count;
            
            for (int i = 0; i < count; i++)
            {
                if (_menuItems[i].IsSelectable())
                    return i;
            }
            
            return -1;
        }
        
        private int FindNextSelectableIndex(int increment)
        {
            if (_selectedIndex == -1)
                return FindFirstSelectableIndex();

            int count = _menuItems.Count;
            int nextIndex = _selectedIndex;

            for (int attempts = 0; attempts < count; attempts++)
            {
                nextIndex = (nextIndex + increment + count) % count;
                if (_menuItems[nextIndex].IsSelectable())
                    return nextIndex;
            }

            return -1;
        }
        
        private bool IsSelectionValid()
        {
            if (_menuItems == null)
                return false;
            
            if(_menuItems.Count == 0)
                return false;
            
            if(_selectedIndex < 0 || _selectedIndex >= _menuItems.Count)
                return false;
            
            return _menuItems[_selectedIndex].IsSelectable();
        }
    }
}