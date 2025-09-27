using System.Collections.Generic;
using System.Linq;
using Gameplay.Combat.Consumables;
using Gameplay.Combat.Data;
using Gameplay.Combat.Skills;
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
        
        public void TryAddReward(RewardDropTable dropTable)
        {
            if (!dropTable)
                return;

            var rewards = new List<DropEntry>(dropTable.EntriesList);

            RemovePlayerWeaponFromSelection(_player.UnitEquipmentData, rewards);
            RemovePlayerArmorFromSelection(_player.UnitEquipmentData, rewards);
            RemovePlayerSkillsFromSelection(_player.UnitSkillsData, rewards);

            if (rewards.Count == 0)
                return;

            var itemReward = SelectWeightedRandom(rewards);
            GiveRewardToPlayer(itemReward);
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

        private void GiveRewardToPlayer(DropEntry dropEntry)
        {
            switch (dropEntry.Item)
            {
                case BaseWeapon weapon:
                    _player.UnitEquipmentData.EquipWeapon(weapon);
                    break;

                case BaseArmor armor:
                    _player.UnitEquipmentData.EquipArmor(armor);
                    break;

                case BaseSkill skill:
                    _player.UnitSkillsData.AddSkill(skill);
                    break;

                case BaseConsumable consumable:
                    _player.UnitInventoryData.AddItems(consumable, dropEntry.Amount);
                    break;

                default:
                    Debug.LogWarning($"Unhandled reward type: {dropEntry.Item.name}");
                    break;
            }
        }
    }
}