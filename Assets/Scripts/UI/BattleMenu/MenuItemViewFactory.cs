using System.Collections.Generic;
using AssetManagement.AssetProviders;
using Constants;
using Gameplay.Services;
using UnityEngine;

namespace UI.BattleMenu
{
    public class MenuItemViewFactory
    {
        private readonly ContainerFactory _factory;
        private readonly UIPrefabsProvider _uiPrefabsProvider;

        public MenuItemViewFactory(ContainerFactory factory, UIPrefabsProvider uiPrefabsProvider)
        {
            _factory = factory;
            _uiPrefabsProvider = uiPrefabsProvider;
        }

        public MenuPageView CreatePage()
        {
            return _factory.Create<MenuPageView>(GetPrefab(ConstPrefabs.MenuPagePrefab));
        }

        public List<BaseMenuItemView> CreateViewsForData(List<MenuItemData> dataList)
        {
            var views = new List<BaseMenuItemView>();

            foreach (var data in dataList)
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

            return views;
        }

        public BaseMenuItemView CreateMenuItem(MenuItemData data)
        {
            var view = _factory.Create<BaseMenuItemView>(GetPrefab(ConstPrefabs.MenuItemViewPrefab));
            view.Bind(data);

            return view;
        }

        private SkillMenuItemView CreateSkillMenuItem(MenuItemData data)
        {
            var view = _factory.Create<SkillMenuItemView>(GetPrefab(ConstPrefabs.SkillMenuItemPrefab));
            view.Bind(data);
            view.SetDescription(data.Description);

            return view;
        }

        private ItemMenuItemView CreateItemMenuItem(MenuItemData data)
        {
            var view = _factory.Create<ItemMenuItemView>(GetPrefab(ConstPrefabs.ItemMenuItemViewPrefab));
            view.Bind(data);
            view.SetDescription(data.Description);
            view.SetQuantity(data.Quantity);

            return view;
        }

        private GameObject GetPrefab(string name)
        {
            return _uiPrefabsProvider.GetPrefab(name);
        }
    }
}