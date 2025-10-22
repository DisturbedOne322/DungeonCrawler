using System.Threading.Tasks;
using Controllers;
using Cysharp.Threading.Tasks;
using Data.UI;
using Gameplay.Consumables;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using Gameplay.Player;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Buffs.DefensiveCore;
using Gameplay.StatusEffects.Buffs.StatBuffsCore;
using Gameplay.StatusEffects.Core;
using Gameplay.Units;
using UnityEngine;

namespace Gameplay.Rewards
{
    public class RewardDistributor
    {
        private readonly EquipmentChangeController _equipmentChangeController;
        private readonly PlayerUnit _player;
        private readonly SkillDiscardController _skillDiscardController;
        private readonly PlayerSkillSlotsManager _skillSlotsManager;

        public RewardDistributor(PlayerUnit player, EquipmentChangeController equipmentChangeController,
            SkillDiscardController skillDiscardController, PlayerSkillSlotsManager skillSlotsManager)
        {
            _player = player;
            _equipmentChangeController = equipmentChangeController;
            _skillDiscardController = skillDiscardController;
            _skillSlotsManager = skillSlotsManager;
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
                    await ProcessSkillReward(skill);
                    break;

                case BaseConsumable consumable:
                    _player.UnitInventoryData.AddItems(consumable, dropEntry.Amount);
                    break;

                case BaseStatusEffectData statusEffect:
                    _player.UnitHeldStatusEffectsData.Add(statusEffect);
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
                if (choice == EquipmentSelectChoice.Change)
                    _player.UnitEquipmentData.EquipWeapon(newWeapon);
            }
            else
            {
                _player.UnitEquipmentData.EquipWeapon(newWeapon);
            }
        }

        private async UniTask ProcessArmorReward(BaseArmor newArmor)
        {
            if (_player.UnitEquipmentData.TryGetEquippedArmor(out var currentArmor))
            {
                var choice = await _equipmentChangeController.MakeEquipmentChoice(currentArmor, newArmor);
                if (choice == EquipmentSelectChoice.Change)
                    _player.UnitEquipmentData.EquipArmor(newArmor);
            }
            else
            {
                _player.UnitEquipmentData.EquipArmor(newArmor);
            }
        }

        private async Task ProcessSkillReward(BaseSkill skill)
        {
            if (_skillSlotsManager.HasFreeSkillSlot())
            {
                _player.UnitSkillsData.AddSkill(skill);
                return;
            }

            var skillToDiscard = await _skillDiscardController.MakeSkillDiscardChoice(skill);
            if (skillToDiscard != skill)
            {
                _player.UnitSkillsData.RemoveSkill(skillToDiscard);
                _player.UnitSkillsData.AddSkill(skill);
            }
        }
    }
}