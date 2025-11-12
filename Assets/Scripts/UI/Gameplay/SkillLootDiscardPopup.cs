using System.Collections.Generic;
using UI.BattleMenu;
using UI.Core;
using UnityEngine;

namespace UI.Gameplay.Experience
{
    public class SkillLootDiscardPopup : BasePopup
    {
        [SerializeField] private RectTransform _oldSkillsParent;
        [SerializeField] private RectTransform _newSkillParent;

        [SerializeField] private SkillMenuItemView _skillPrefab;
        [SerializeField] private MenuItemsScroller _menuItemsScroller;

        public void SetData(SkillDiscardMenuUpdater skillDiscardMenuUpdater)
        {
            var playerSkills = skillDiscardMenuUpdater.MenuItems;
            var newSkill = skillDiscardMenuUpdater.NewSkillData;

            List<BaseMenuItemView> list = new();

            foreach (var skill in playerSkills)
            {
                var view = Instantiate(_skillPrefab, _oldSkillsParent, false);
                view.Bind(skill);
                view.SetDescription(skill.Description);

                list.Add(view);
            }

            var newView = Instantiate(_skillPrefab, _newSkillParent, false);
            newView.Bind(newSkill);
            newView.SetDescription(newSkill.Description);

            _menuItemsScroller.SetData(list, skillDiscardMenuUpdater);
        }
    }
}