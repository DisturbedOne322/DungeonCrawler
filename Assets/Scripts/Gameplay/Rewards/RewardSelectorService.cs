using System.Collections.Generic;
using System.Linq;
using Gameplay.Facades;
using Gameplay.Units;
using Helpers;
using Random = System.Random;

namespace Gameplay.Rewards
{
    public class RewardSelectorService
    {
        private readonly PlayerUnit _player;
        private readonly Random _rng = new();

        public RewardSelectorService(PlayerUnit player)
        {
            _player = player;
        }

        public DropEntry SelectReward(RewardDropTable dropTable)
        {
            if (!dropTable)
                return null;

            var rewards = new List<DropEntry>(dropTable.EntriesList);

            RemovePlayerWeaponFromSelection(_player, rewards);
            RemovePlayerArmorFromSelection(_player, rewards);
            RemovePlayerSkillsFromSelection(_player, rewards);
            RemoveStatusEffectsFromSelection(_player, rewards);

            if (rewards.Count == 0)
                return dropTable.FallbackItem;

            var itemReward = SelectWeightedRandom(rewards);
            return itemReward;
        }

        private void RemovePlayerSkillsFromSelection(IGameUnit gameUnit, List<DropEntry> allRewards)
        {
            for (var i = allRewards.Count - 1; i >= 0; i--)
            {
                var reward = allRewards[i];
                if (UnitInventoryHelper.HasItem(gameUnit, reward.Item))
                    allRewards.RemoveAt(i);
            }
        }

        private void RemovePlayerWeaponFromSelection(IGameUnit unit,  
            List<DropEntry> allRewards)
        {
            for (var i = allRewards.Count - 1; i >= 0; i--)
            {
                var reward = allRewards[i];
                if (UnitInventoryHelper.HasItem(unit, reward.Item))
                    allRewards.RemoveAt(i);
            }
        }

        private void RemovePlayerArmorFromSelection(IGameUnit unit,  List<DropEntry> allRewards)
        {
            for (var i = allRewards.Count - 1; i >= 0; i--)
            {
                var reward = allRewards[i];
                if (UnitInventoryHelper.HasItem(unit, reward.Item))
                    allRewards.RemoveAt(i);
            }
        }
        
        private void RemoveStatusEffectsFromSelection(IGameUnit unit, List<DropEntry> allRewards)
        {
            for (var i = allRewards.Count - 1; i >= 0; i--)
            {
                var reward = allRewards[i];
                
                if (UnitInventoryHelper.HasItem(unit, reward.Item))
                    allRewards.RemoveAt(i);
            }
        }

        private DropEntry SelectWeightedRandom(List<DropEntry> entries)
        {
            var totalWeight = 0;
            
            foreach (var e in entries) 
                totalWeight += e.Weight;

            var roll = _rng.Next(0, totalWeight);

            foreach (var entry in entries)
            {
                roll -= entry.Weight;
                if (roll < 0)
                    return entry;
            }

            return entries.Last();
        }
    }
}