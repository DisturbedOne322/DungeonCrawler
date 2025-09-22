using System.Collections.Generic;
using UniRx;

namespace StateMachine.BattleMenu
{
    public class MenuItemsUpdater
    {
        public ReactiveProperty<int> SelectedIndex = new();

        private List<BattleMenuItemData> _menuItems;
        public List<BattleMenuItemData> MenuItems => _menuItems;
        
        public void SetMenuItems(List<BattleMenuItemData> menuItems) => _menuItems = menuItems;
        
        public void UpdateSelection(int increment = 0)
        {
            int count = _menuItems.Count;
            
            if (count == 0) 
                return;

            SelectedIndex.Value = (SelectedIndex.Value + increment + count) % count;

            for (int i = 0; i < count; i++)
            {
                var item = _menuItems[i];
                
                if (!item.IsSelectable())
                {
                    item.IsHighlighted.Value = false;
                    continue;
                }
                
                item.IsHighlighted.Value = i == SelectedIndex.Value;
            }
        }

        public void ResetSelection() => SelectedIndex.Value = 0;

        public void ExecuteSelection()
        {
            if (_menuItems.Count == 0) 
                return;
            
            _menuItems[SelectedIndex.Value].OnSelected?.Invoke();
        }
    }
}