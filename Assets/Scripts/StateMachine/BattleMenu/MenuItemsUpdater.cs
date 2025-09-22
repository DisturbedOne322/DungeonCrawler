using System.Collections.Generic;

namespace StateMachine.BattleMenu
{
    public class MenuItemsUpdater
    {
        private int _selectedIndex;
        public int SelectedIndex => _selectedIndex;
        
        public void UpdateSelection(List<BattleMenuItem> items, int increment = 0)
        {
            if (items.Count == 0) return;

            _selectedIndex = (_selectedIndex + increment + items.Count) % items.Count;

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                
                if (!item.IsSelectable())
                {
                    item.IsHighlighted.Value = false;
                    continue;
                }
                
                item.IsHighlighted.Value = i == _selectedIndex;
            }
        }

        public void ResetSelection(List<BattleMenuItem> items)
        {
            _selectedIndex = 0;
            UpdateSelection(items);
        }

        public void ExecuteSelection(List<BattleMenuItem> items)
        {
            if (items.Count == 0) 
                return;
            
            items[_selectedIndex].OnSelected?.Invoke();
        }
    }
}