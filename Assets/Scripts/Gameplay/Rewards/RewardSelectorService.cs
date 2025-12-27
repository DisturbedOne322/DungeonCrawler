using System.Collections.Generic;
using System.Linq;
using AssetManagement.AssetProviders.ConfigProviders;
using Gameplay.Facades;
using Gameplay.Units;
using Helpers;
using Random = System.Random;

namespace Gameplay.Rewards
{
    public class RewardSelectorService
    {
        private readonly PlayerUnit _player;
        private readonly GameplayConfigsProvider _configProvider;
        
        private readonly Random _rng = new();

        public RewardSelectorService(PlayerUnit player, GameplayConfigsProvider configProvider)
        {
            _player = player;
            _configProvider = configProvider;
        }

        public DropEntry SelectReward(RewardDropTable dropTable)
        {
            if (!dropTable)
                return null;

            var rewards = new List<DropEntry>(dropTable.EntriesList);
            
            RemovePlayerItemsFromSelection(_player, rewards);
            
            if (rewards.Count == 0)
                return dropTable.FallbackItem;

            var itemReward = SelectDrop(rewards);
            
            if(itemReward == null)
                return dropTable.FallbackItem;
            
            return itemReward;
        }

        private void RemovePlayerItemsFromSelection(IGameUnit gameUnit, List<DropEntry> allRewards)
        {
            for (var i = allRewards.Count - 1; i >= 0; i--)
            {
                var reward = allRewards[i];
                if (UnitInventoryHelper.HasItem(gameUnit, reward.Item))
                    allRewards.RemoveAt(i);
            }
        }
        
        private Dictionary<ItemRarity, List<DropEntry>> GroupByRarity(List<DropEntry> entries)
        {
            return entries
                .GroupBy(e => e.Item.Rarity)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
        
        private ItemRarity RollRarity(int luck, IEnumerable<ItemRarity> availableRarities)
        {
            var weighted = new List<(ItemRarity rarity, int weight)>();

            var rarityLuckTable = _configProvider.GetConfig<LuckTableConfig>();
            
            foreach (var rarity in availableRarities)
            {
                int weight = rarityLuckTable.GetWeight(rarity, luck);
                if (weight > 0)
                    weighted.Add((rarity, weight));
            }

            int total = weighted.Sum(w => w.weight);
            int roll = _rng.Next(0, total);

            foreach (var entry in weighted)
            {
                roll -= entry.weight;
                if (roll < 0)
                    return entry.rarity;
            }

            return weighted.Last().rarity;
        }
        
        private DropEntry SelectDrop(List<DropEntry> rewards)
        {
            int luck = _player.UnitStatsData.Luck.Value;

            var grouped = GroupByRarity(rewards);
            var rolledRarity = RollRarity(luck, grouped.Keys);

            var candidates = grouped[rolledRarity];
            return candidates[_rng.Next(candidates.Count)];
        }
    }
}