using System.Collections.Generic;
using Gameplay.Services;
using StateMachine.BattleMenu;
using UnityEngine;
using Zenject;

namespace UI.BattleMenu
{
    public class MenuItemViewFactory : MonoBehaviour
    {
        [SerializeField] private MenuPageView _pagePrefab;
        [SerializeField] private BaseMenuItemView _menuItemPrefab;
        [SerializeField] private SkillMenuItemView _skillMenuItemPrefab;
        [SerializeField] private ItemMenuItemView _itemMenuItemPrefab;

        private ContainerFactory _factory;

        [Inject]
        private void Construct(ContainerFactory factory)
        {
            _factory = factory;
        }

        public MenuPageView CreatePage() => _factory.Create<MenuPageView>(_pagePrefab.gameObject);

        public List<BaseMenuItemView> CreateViewsForData(List<MenuItemData> dataList)
        {
            List<BaseMenuItemView> views = new List<BaseMenuItemView>();

            foreach (var data in dataList)
            {
                switch (data.Type)
                {
                    case MenuItemType.Submenu:
                        views.Add(CreateMenuItem(data));
                        break;
                    case MenuItemType.Skill:
                        views.Add(CreateSkillMenuItem(data));
                        break;
                    case MenuItemType.Item:
                        views.Add(CreateItemMenuItem(data));
                        break;
                }
            }
            
            return views;
        }
        
        private BaseMenuItemView CreateMenuItem(MenuItemData data)
        {
            var view = _factory.Create<BaseMenuItemView>(_menuItemPrefab.gameObject);
            view.Bind(data);
            
            return view;
        }
        
        private SkillMenuItemView CreateSkillMenuItem(MenuItemData data)
        {
            var view = _factory.Create<SkillMenuItemView>(_skillMenuItemPrefab.gameObject);
            view.Bind(data);
            view.SetDescription(data.Description);
            
            return view;
        }
        
        private ItemMenuItemView CreateItemMenuItem(MenuItemData data)
        {
            var view = _factory.Create<ItemMenuItemView>(_itemMenuItemPrefab.gameObject);
            view.Bind(data);
            view.SetDescription(data.Description);
            view.SetQuantity(data.Quantity);
            
            return view;
        }
    }
}