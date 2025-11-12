using System.Collections.Generic;
using UI.BattleMenu;
using UI.Core;
using UnityEngine;

namespace UI.Gameplay
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