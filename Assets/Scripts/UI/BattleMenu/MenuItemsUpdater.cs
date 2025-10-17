using System.Collections.Generic;
using UniRx;

namespace UI.BattleMenu
{
    public class MenuItemsUpdater
    {
        public Subject<Unit> OnViewChanged = new();

        protected int SelectedIndex;
        public List<MenuItemData> MenuItems { get; private set; }

        public int GetSelectedIndex()
        {
            return SelectedIndex;
        }

        public int GetFirstSelectableIndex()
        {
            return FindFirstSelectableIndex();
        }

        public int GetLastSelectableIndex()
        {
            return FindLastSelectableIndex();
        }

        public bool IsSelectionValid()
        {
            return CheckSelectionValid();
        }

        public void SetMenuItems(List<MenuItemData> menuItems)
        {
            MenuItems = menuItems;
        }

        public void UpdateSelection(int increment)
        {
            var count = MenuItems.Count;

            if (count == 0)
                return;

            SelectedIndex = FindNextSelectableIndex(increment);
            if (SelectedIndex == -1)
                return;

            ApplyHighlight();
        }

        public void ResetSelection(bool rememberSelection = true)
        {
            if (MenuItems == null)
                return;

            if (!rememberSelection || !CheckSelectionValid())
                SelectedIndex = FindFirstSelectableIndex();

            ApplyHighlight();
        }

        public virtual void ExecuteSelection()
        {
            if (!CheckSelectionValid())
                return;

            MenuItems[SelectedIndex].OnSelected?.Invoke();
        }

        protected void ApplyHighlight()
        {
            for (var i = 0; i < MenuItems.Count; i++)
            {
                var item = MenuItems[i];
                item.IsHighlighted.Value = i == SelectedIndex && item.IsSelectable();
            }

            OnViewChanged?.OnNext(default);
        }

        private int FindFirstSelectableIndex()
        {
            var count = MenuItems.Count;

            for (var i = 0; i < count; i++)
                if (MenuItems[i].IsSelectable())
                    return i;

            return -1;
        }

        private int FindLastSelectableIndex()
        {
            var count = MenuItems.Count;

            for (var i = count - 1; i >= 0; i--)
                if (MenuItems[i].IsSelectable())
                    return i;

            return -1;
        }

        private int FindNextSelectableIndex(int increment)
        {
            if (SelectedIndex == -1)
                return FindFirstSelectableIndex();

            var count = MenuItems.Count;
            var nextIndex = SelectedIndex;

            for (var i = 0; i < count; i++)
            {
                nextIndex = (nextIndex + increment + count) % count;
                if (MenuItems[nextIndex].IsSelectable())
                    return nextIndex;
            }

            return -1;
        }

        private bool CheckSelectionValid()
        {
            if (MenuItems == null)
                return false;

            if (MenuItems.Count == 0)
                return false;

            if (SelectedIndex < 0 || SelectedIndex >= MenuItems.Count)
                return false;

            return MenuItems[SelectedIndex].IsSelectable();
        }
    }
}