using System.Collections.Generic;
using Gameplay.Combat.Skills;
using Gameplay.Services;
using UnityEngine;
using Zenject;

namespace UI.PlayerBattleMenu
{
    public class MenuItemViewFactory : MonoBehaviour
    {
        [SerializeField] private SkillMenuItemView _skillPrefab;
        [SerializeField] private MenuItemView _menuItemPrefab;

        private ContainerFactory _containerFactory;

        [Inject]
        private void Construct(ContainerFactory containerFactory)
        {
            _containerFactory = containerFactory;
        }
        
        public SkillMenuItemView CreateSkillItemView(BaseSkill skill)
        {
            var view = _containerFactory.Create<SkillMenuItemView>(_skillPrefab.gameObject);
            view.SetText(skill.Name);
            view.SetDescription(skill.Description);
            return view;
        }
        
        public MenuItemView CreateMenuItemView(string text)
        {
            var view = _containerFactory.Create<MenuItemView>(_menuItemPrefab.gameObject);
            view.SetText(text);
            return view;
        }
    }
}