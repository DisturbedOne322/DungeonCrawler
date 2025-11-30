using System.Collections.Generic;
using UI.Core;
using UI.Menus;
using UI.Menus.MenuItemViews;
using UnityEngine;

namespace UI.Popups
{
    public class SkillPurchaseDiscardPopup : BasePopup
    {
        [SerializeField] private RectTransform _oldSkillsParent;

        [SerializeField] private SkillMenuItemView _skillPrefab;
        [SerializeField] private MenuItemsScroller _menuItemsScroller;

        public void SetData(MenuItemsUpdater menuItemsUpdater)
        {
            var playerSkills = menuItemsUpdater.MenuItems;

            List<BaseMenuItemView> list = new();

            foreach (var skill in playerSkills)
            {
                var view = Instantiate(_skillPrefab, _oldSkillsParent, false);
                view.Bind(skill);
                view.SetDescription(skill.Description);

                list.Add(view);
            }

            _menuItemsScroller.SetData(list, menuItemsUpdater);
        }
    }
}