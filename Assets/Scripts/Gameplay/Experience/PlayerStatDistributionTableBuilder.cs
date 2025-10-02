using System.Collections.Generic;
using AssetManagement.AssetProviders;
using Constants;
using Gameplay.Services;
using UI.BattleMenu;
using UI.Stats;
using UniRx;

namespace Gameplay.Experience
{
    public class PlayerStatDistributionTableBuilder
    {
        private readonly UIPrefabsProvider _uiPrefabsProvider;
        private readonly ContainerFactory _containerFactory;
        private readonly MenuItemViewFactory _menuFactory;

        public PlayerStatDistributionTableBuilder(UIPrefabsProvider uiPrefabsProvider,
            ContainerFactory containerFactory, MenuItemViewFactory menuFactory)
        {
            _uiPrefabsProvider = uiPrefabsProvider;
            _containerFactory = containerFactory;
            _menuFactory = menuFactory;
        }

        public List<MenuItemData> CreateItemDataList(List<string> statNames)
        {
            List<MenuItemData> menuItems = new();

            foreach (var name in statNames)
            {
                menuItems.Add(new MenuItemData(
                    label: name,
                    () => true,
                    () => {},
                    MenuItemType.Stat
                ));
            }
            
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

        private void AddEntry(MenuItemData itemData, ReactiveProperty<int> statValue, Dictionary<BaseMenuItemView, StatIncreaseView> dictionary)
        {
            var statIncreasePrefab = _uiPrefabsProvider.GetPrefab(ConstPrefabs.StatIncreaseDisplayPrefab);
            var statView = _containerFactory.Create<StatIncreaseView>(statIncreasePrefab);

            var view = _menuFactory.CreateMenuItem(itemData);
            statView.Initialize(statValue);
            
            dictionary.Add(view, statView);
        }
    }
}