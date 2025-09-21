using System.Collections.Generic;

namespace UI.PlayerBattleMenu
{
    public class MenuNavigator
    {
        private int _selectedIndex;

        public void UpdateSelection(List<MenuItemView> items, int increment = 0)
        {
            if (items.Count == 0) return;

            _selectedIndex = (_selectedIndex + increment + items.Count) % items.Count;

            for (int i = 0; i < items.Count; i++)
            {
                if (!items[i].Data.IsSelectable())
                    items[i].SetUnusable();

                items[i].SetSelected(i == _selectedIndex);
            }
        }

        public void ResetSelection(List<MenuItemView> items)
        {
            _selectedIndex = 0;
            UpdateSelection(items);
        }

        public void ExecuteSelection(List<MenuItemView> items)
        {
            if (items.Count == 0) return;
            items[_selectedIndex].Data.OnSelected?.Invoke();
        }
    }
}