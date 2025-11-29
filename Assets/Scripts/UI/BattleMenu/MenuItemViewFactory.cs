using System.Collections.Generic;
using AssetManagement.AssetProviders;
using Constants;
using Gameplay.Dungeon.Rooms.BasePurchasableItems;
using Gameplay.Services;
using UI.Core;
using UI.Menus;
using UI.Menus.MenuItemViews;
using UnityEngine;

namespace UI.BattleMenu
{
    public class MenuItemViewFactory
    {
        private readonly ContainerFactory _factory;
        private readonly MenuItemPrefabsRegistry _prefabRegistry;
        private readonly UIPrefabsProvider _uiPrefabsProvider;

        public MenuItemViewFactory(ContainerFactory factory, MenuItemPrefabsRegistry prefabRegistry,
            UIPrefabsProvider uiPrefabsProvider)
        {
            _factory = factory;
            _prefabRegistry = prefabRegistry;
            _uiPrefabsProvider = uiPrefabsProvider;
        }

        public MenuPageView CreatePage()
        {
            return _factory.Create<MenuPageView>(_uiPrefabsProvider.GetPrefab(ConstPrefabs.MenuPagePrefab));
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
                        views.Add(CreateShopItemMenuItem(data as PurchasableItemMenuItemData));
                        break;
                }

            return views;
        }

        public BaseMenuItemView CreateMenuItem(MenuItemData data)
        {
            var view = _factory.Create<BaseMenuItemView>(GetPrefab<BaseMenuItemView>());
            view.Bind(data);

            return view;
        }

        public StatusEffectMenuItemView CreateStatusEffectMenuItem(StatusEffectMenuItemData data)
        {
            var view =  _factory.Create<StatusEffectMenuItemView>(GetPrefab<StatusEffectMenuItemView>());
            view.Bind(data);
            view.SetDescription(data.Description);
            view.SetIcon(data.StatusEffect.Icon);
            return view;
        }

        private SkillMenuItemView CreateSkillMenuItem(MenuItemData data)
        {
            var view = _factory.Create<SkillMenuItemView>(GetPrefab<SkillMenuItemView>());
            view.Bind(data);
            view.SetDescription(data.Description);

            return view;
        }

        private ConsumableMenuItemView CreateConsumableMenuItem(MenuItemData data)
        {
            var view = _factory.Create<ConsumableMenuItemView>(GetPrefab<ConsumableMenuItemView>());
            view.Bind(data);
            view.SetDescription(data.Description);
            view.SetQuantity(data.OriginalQuantity);

            return view;
        }

        private ShopItemMenuItemView CreateShopItemMenuItem(PurchasableItemMenuItemData data)
        {
            var view = _factory.Create<ShopItemMenuItemView>(GetPrefab<ShopItemMenuItemView>());
            view.Bind(data);
            view.SetData(data);

            return view;
        }

        private GameObject GetPrefab<T>() where  T : BaseMenuItemView
        {
            return _prefabRegistry.GetMenuItemPrefab<T>().gameObject;
        }
    }
}