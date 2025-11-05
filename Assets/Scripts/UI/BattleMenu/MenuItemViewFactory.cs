using System.Collections.Generic;
using AssetManagement.AssetProviders;
using Constants;
using Gameplay.Dungeon.Rooms.BaseSellableItems;
using Gameplay.Services;
using UI.Menus;
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
                    case MenuItemType.Consumable:
                        views.Add(CreateConsumableMenuItem(data));
                        break;
                    case MenuItemType.ShopItem:
                        views.Add(CreateShopItemMenuItem(data as SoldItemMenuItemData));
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

        private ConsumableMenuItemView CreateConsumableMenuItem(MenuItemData data)
        {
            var view = _factory.Create<ConsumableMenuItemView>(GetPrefab(ConstPrefabs.ItemMenuItemViewPrefab));
            view.Bind(data);
            view.SetDescription(data.Description);
            view.SetQuantity(data.OriginalQuantity);

            return view;
        }
        
        private ShopItemMenuItemView CreateShopItemMenuItem(SoldItemMenuItemData data)
        {
            var view = _factory.Create<ShopItemMenuItemView>(GetPrefab(ConstPrefabs.ShopItemMenuItemViewPrefab));
            view.Bind(data);
            view.SetData(data);

            return view;
        }

        private GameObject GetPrefab(string name)
        {
            return _uiPrefabsProvider.GetPrefab(name);
        }
    }
}