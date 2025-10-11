using System.Collections.Generic;
using System.Linq;
using Gameplay.Combat.Data;
using Gameplay.Equipment;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Rewards
{
    public class RewardSelectorService
    {
        private readonly PlayerUnit _player;
        private readonly System.Random _rng = new();
        
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

            if (rewards.Count == 0)
                return null;

            var itemReward = SelectWeightedRandom(rewards);
            return itemReward;
        }

        private void RemovePlayerSkillsFromSelection(UnitSkillsData skillsData, List<DropEntry> skillDrops)
        {
            for (int i = skillDrops.Count - 1; i >= 0; i--)
            {
                var reward = skillDrops[i];
                if(skillsData.Skills.Contains(reward.Item))
                    skillDrops.RemoveAt(i);
            }
        }

        private void RemovePlayerWeaponFromSelection(UnitEquipmentData playerEquipmentData, List<DropEntry> weaponRewards)
        {
            if (!playerEquipmentData.TryGetEquippedWeapon(out var playerWeapon))
                return;
            
            for (int i = weaponRewards.Count - 1; i >= 0; i--)
            {
                var reward = weaponRewards[i];
                if(reward.Item == playerWeapon)
                    weaponRewards.RemoveAt(i);
            }
        }

        private void RemovePlayerArmorFromSelection(UnitEquipmentData playerEquipmentData, List<DropEntry> armorRewards)
        {
            if (!playerEquipmentData.TryGetEquippedArmor(out var playerArmor)) 
                return;
            
            for (int i = armorRewards.Count - 1; i >= 0; i--)
            {
                var reward = armorRewards[i];
                if(reward.Item == playerArmor)
                    armorRewards.RemoveAt(i);
            }
        }
        
        private DropEntry SelectWeightedRandom(List<DropEntry> entries)
        {
            int totalWeight = entries.Sum(e => e.Weight);
            int roll = _rng.Next(0, totalWeight);

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