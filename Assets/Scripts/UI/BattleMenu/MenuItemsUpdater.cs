using System.Collections.Generic;
using UniRx;

namespace UI.BattleMenu
{
    public class MenuItemsUpdater
    {
        private List<MenuItemData> _menuItems;
        public List<MenuItemData> MenuItems => _menuItems;

        protected int SelectedIndex = 0;

        public Subject<Unit> OnViewChanged = new();

        public int GetSelectedIndex() => SelectedIndex;
        public int GetFirstSelectableIndex() => FindFirstSelectableIndex();
        public int GetLastSelectableIndex() => FindLastSelectableIndex();
        
        public bool IsSelectionValid() => CheckSelectionValid();

        public void SetMenuItems(List<MenuItemData> menuItems) => _menuItems = menuItems;

        public void UpdateSelection(int increment)
        {
            int count = _menuItems.Count;
            
            if (count == 0) 
                return;

            SelectedIndex = FindNextSelectableIndex(increment);
            if (SelectedIndex == -1)
                return;

            ApplyHighlight();
        }

        public void ResetSelection(bool rememberSelection = true)
        {
            if(_menuItems == null)
                return;
            
            if (!rememberSelection || !CheckSelectionValid()) 
                SelectedIndex = FindFirstSelectableIndex();

            ApplyHighlight();
        }

        public virtual void ExecuteSelection()
        {
            if (!CheckSelectionValid()) 
                return;
            
            _menuItems[SelectedIndex].OnSelected?.Invoke();
        }
        
        protected void ApplyHighlight()
        {
            for (int i = 0; i < _menuItems.Count; i++)
            {
                var item = _menuItems[i];
                item.IsHighlighted.Value = (i == SelectedIndex && item.IsSelectable());
            }
            
            OnViewChanged?.OnNext(default);
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

        private int FindLastSelectableIndex()
        {
            int count = _menuItems.Count;
            
            for (int i = count - 1; i >= 0; i--)
            {
                if (_menuItems[i].IsSelectable())
                    return i;
            }
            
            return -1;
        }
        
        private int FindNextSelectableIndex(int increment)
        {
            if (SelectedIndex == -1)
                return FindFirstSelectableIndex();

            int count = _menuItems.Count;
            int nextIndex = SelectedIndex;

            for (int i = 0; i < count; i++)
            {
                nextIndex = (nextIndex + increment + count) % count;
                if (_menuItems[nextIndex].IsSelectable())
                    return nextIndex;
            }

            return -1;
        }
        
        private bool CheckSelectionValid()
        {
            if (_menuItems == null)
                return false;
            
            if(_menuItems.Count == 0)
                return false;
            
            if(SelectedIndex < 0 || SelectedIndex >= _menuItems.Count)
                return false;
            
            return _menuItems[SelectedIndex].IsSelectable();
        }
    }
}