using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.Units;

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

            RemovePlayerWeaponFromSelection(_player.UnitEquipmentData, rewards);
            RemovePlayerArmorFromSelection(_player.UnitEquipmentData, rewards);
            RemovePlayerSkillsFromSelection(_player.UnitSkillsData, rewards);
            RemoveStatusEffectsFromSelection(_player.UnitHeldStatusEffectsData, rewards);
            
            if (rewards.Count == 0)
                return null;

            var itemReward = SelectWeightedRandom(rewards);
            return itemReward;
        }

        private void RemovePlayerSkillsFromSelection(UnitSkillsData skillsData, List<DropEntry> allRewards)
        {
            for (var i = allRewards.Count - 1; i >= 0; i--)
            {
                var reward = allRewards[i];
                if (skillsData.Skills.Contains(reward.Item))
                    allRewards.RemoveAt(i);
            }
        }

        private void RemovePlayerWeaponFromSelection(UnitEquipmentData playerEquipmentData,
            List<DropEntry> allRewards)
        {
            if (!playerEquipmentData.TryGetEquippedWeapon(out var playerWeapon))
                return;

            for (var i = allRewards.Count - 1; i >= 0; i--)
            {
                var reward = allRewards[i];
                if (reward.Item == playerWeapon)
                    allRewards.RemoveAt(i);
            }
        }

        private void RemovePlayerArmorFromSelection(UnitEquipmentData playerEquipmentData, List<DropEntry> allRewards)
        {
            if (!playerEquipmentData.TryGetEquippedArmor(out var playerArmor))
                return;

            for (var i = allRewards.Count - 1; i >= 0; i--)
            {
                var reward = allRewards[i];
                if (reward.Item == playerArmor)
                    allRewards.RemoveAt(i);
            }
        }
        
        private void RemoveStatusEffectsFromSelection(UnitHeldStatusEffectsData statusEffectsData, List<DropEntry> allRewards)
        {
            for (var i = allRewards.Count - 1; i >= 0; i--)
            {
                var reward = allRewards[i];
                if (statusEffectsData.All.Contains(reward.Item))
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