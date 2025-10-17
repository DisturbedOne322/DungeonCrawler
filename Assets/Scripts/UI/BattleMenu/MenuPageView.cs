using System.Collections.Generic;
using UnityEngine;

namespace UI.BattleMenu
{
    public class MenuPageView : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private MenuItemsScroller _menuItemsScroller;

        public void SetItems(List<BaseMenuItemView> items, MenuItemsUpdater updater)
        {
            foreach (var item in items)
                item.transform.SetParent(_content, false);

            _menuItemsScroller.SetData(items, updater);
        }
    }
}