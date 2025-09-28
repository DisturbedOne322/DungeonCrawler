using System.Threading.Tasks;
using Controllers;
using Cysharp.Threading.Tasks;
using Data.UI;
using Gameplay.Combat.Consumables;
using Gameplay.Combat.Skills;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Rewards
{
    public class RewardDistributor
    {
        private readonly PlayerUnit _player;
        private readonly EquipmentChangeController _equipmentChangeController;

        public RewardDistributor(PlayerUnit player, EquipmentChangeController equipmentChangeController)
        {
            _player = player;
            _equipmentChangeController = equipmentChangeController;
        }
        
        public async UniTask GiveRewardToPlayer(DropEntry dropEntry)
        {
            if (dropEntry == null)
                return;
            
            switch (dropEntry.Item)
            {
                case BaseWeapon weapon:
                    await ProcessWeaponReward(weapon);
                    break;

                case BaseArmor armor:
                    await ProcessArmorReward(armor);
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

        private async UniTask ProcessWeaponReward(BaseWeapon newWeapon)
        {
            if (_player.UnitEquipmentData.TryGetEquippedWeapon(out var currentWeapon))
            {
                var choice = await _equipmentChangeController.MakeEquipmentChoice(currentWeapon, newWeapon);
                if(choice == EquipmentSelectChoice.Change)
                    _player.UnitEquipmentData.EquipWeapon(newWeapon);
            }
            else
                _player.UnitEquipmentData.EquipWeapon(newWeapon);
        }
        
        private async UniTask ProcessArmorReward(BaseArmor newArmor)
        {
            if (_player.UnitEquipmentData.TryGetEquippedArmor(out var currentArmor))
            {
                var choice = await _equipmentChangeController.MakeEquipmentChoice(currentArmor, newArmor);
                if(choice == EquipmentSelectChoice.Change)
                    _player.UnitEquipmentData.EquipArmor(newArmor);
            }
            else
                _player.UnitEquipmentData.EquipArmor(newArmor);
        }
    }
}