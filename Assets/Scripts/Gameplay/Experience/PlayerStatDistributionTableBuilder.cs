using System.Collections.Generic;
using AssetManagement.AssetProviders;
using Constants;
using Gameplay.Services;
using UI.Stats;
using UniRx;

namespace Gameplay.Experience
{
    public class PlayerStatDistributionTableBuilder
    {
        private readonly UIPrefabsProvider _uiPrefabsProvider;
        private readonly ContainerFactory _containerFactory;

        public PlayerStatDistributionTableBuilder(UIPrefabsProvider uiPrefabsProvider,
            ContainerFactory containerFactory)
        {
            _uiPrefabsProvider = uiPrefabsProvider;
            _containerFactory = containerFactory;
        }

        public Dictionary<StatNameDisplay, StatIncreaseView> GetStatsTable(
            Dictionary<string, ReactiveProperty<int>> playerStats)
        {
            Dictionary<StatNameDisplay, StatIncreaseView> result = new();

            foreach (var statKv in playerStats) 
                AddEntry(statKv.Key, statKv.Value, result);
            
            return result;
        }

        private void AddEntry(string statName, ReactiveProperty<int> statValue, Dictionary<StatNameDisplay, StatIncreaseView> dictionary)
        {
            var statNamePrefab = _uiPrefabsProvider.GetPrefab(ConstPrefabs.StatNameDisplayPrefab);
            var statIncreasePrefab = _uiPrefabsProvider.GetPrefab(ConstPrefabs.StatIncreaseDisplayPrefab);

            var nameView = _containerFactory.Create<StatNameDisplay>(statNamePrefab);
            var statView = _containerFactory.Create<StatIncreaseView>(statIncreasePrefab);
            
            nameView.Initialize(statName);
            statView.Initialize(statValue);
            
            dictionary.Add(nameView, statView);
        }
    }
}