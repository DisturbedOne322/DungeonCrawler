using System.Collections.Generic;
using UniRx;
using UnityEngine;

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

        public void SetMenuItems(List<MenuItemData> menuItems, bool rememberSelection = true)
        {
            MenuItems = menuItems;
            ResetSelection(rememberSelection);
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

            UpdateItemsSelectable();

            if (!rememberSelection || !CheckSelectionValid())
                SelectedIndex = FindFirstSelectableIndex();

            ApplyHighlight();
        }

        public virtual void ExecuteSelection()
        {
            if (!CheckSelectionValid())
                return;

            MenuItems[SelectedIndex].OnSelected?.Invoke();

            UpdateItemsSelectable();
            if (!CheckSelectionValid())
            {
                SelectedIndex = FindClosestSelectableIndex();
                ApplyHighlight();
            }
        }

        private void UpdateItemsSelectable()
        {
            for (var i = MenuItems.Count - 1; i >= 0; i--)
            {
                var item = MenuItems[i];
                item.IsSelectable.Value = item.SelectableFunc.Invoke();
            }
        }

        protected void ApplyHighlight()
        {
            for (var i = 0; i < MenuItems.Count; i++)
            {
                var item = MenuItems[i];
                var shouldHighlight = i == SelectedIndex;
                item.IsHighlighted.Value = shouldHighlight;
            }

            OnViewChanged?.OnNext(default);
        }

        private int FindClosestSelectableIndex()
        {
            var current = SelectedIndex;

            var nextClosest = FindNextSelectableIndex(+1);
            var prevClosest = FindNextSelectableIndex(-1);

            if (Mathf.Abs(current - nextClosest) > Mathf.Abs(current - prevClosest))
                return prevClosest;

            return nextClosest;
        }

        private int FindFirstSelectableIndex()
        {
            var count = MenuItems.Count;

            for (var i = 0; i < count; i++)
                if (MenuItems[i].IsSelectable.Value)
                    return i;

            return -1;
        }

        private int FindLastSelectableIndex()
        {
            var count = MenuItems.Count;

            for (var i = count - 1; i >= 0; i--)
                if (MenuItems[i].IsSelectable.Value)
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
                if (MenuItems[nextIndex].IsSelectable.Value)
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

            return MenuItems[SelectedIndex].IsSelectable.Value;
        }
    }
}