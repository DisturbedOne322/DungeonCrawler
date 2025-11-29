using System.Collections.Generic;
using AssetManagement.AssetProviders;
using Constants;
using UI.Menus;
using UI.Menus.Data;
using UI.Menus.MenuItemViews;
using UI.Stats;
using UniRx;

namespace Gameplay.Services.Stats
{
    public class PlayerStatTableBuilder
    {
        private readonly ContainerFactory _containerFactory;
        private readonly MenuItemViewFactory _menuFactory;
        private readonly UIPrefabsProvider _uiPrefabsProvider;
        private readonly PlayerStatsProvider _playerStatProvider;

        public PlayerStatTableBuilder(UIPrefabsProvider uiPrefabsProvider,
            ContainerFactory containerFactory, MenuItemViewFactory menuFactory,
            PlayerStatsProvider playerStatProvider)
        {
            _uiPrefabsProvider = uiPrefabsProvider;
            _containerFactory = containerFactory;
            _menuFactory = menuFactory;
            _playerStatProvider = playerStatProvider;
        }

        public Dictionary<MenuItemData, ReactiveProperty<int>> CreateMenuItems()
        {
            Dictionary<MenuItemData, ReactiveProperty<int>> menuItems = new();

            var playerStats = _playerStatProvider.GetPlayerStats();
            
            foreach (var name in playerStats.Keys)
                menuItems.Add(new MenuItemData(
                    name,
                    () => true,
                    () => { },
                    MenuItemType.Stat
                ), playerStats[name]);

            return menuItems;
        }

        public Dictionary<BaseMenuItemView, StatIncreaseView> GetStatsTable(
            Dictionary<MenuItemData, ReactiveProperty<int>> playerStats)
        {
            Dictionary<BaseMenuItemView, StatIncreaseView> result = new();

            foreach (var statKv in playerStats)
                AddEntry(statKv.Key, statKv.Value, result);

            return result;
        }

        private void AddEntry(MenuItemData itemData, ReactiveProperty<int> statValue,
            Dictionary<BaseMenuItemView, StatIncreaseView> dictionary)
        {
            var statIncreasePrefab = _uiPrefabsProvider.GetPrefab(ConstPrefabs.StatIncreaseDisplayPrefab);
            var statView = _containerFactory.Create<StatIncreaseView>(statIncreasePrefab);

            var view = _menuFactory.CreateMenuItem(itemData);
            statView.Initialize(statValue);

            dictionary.Add(view, statView);
        }
    }
}