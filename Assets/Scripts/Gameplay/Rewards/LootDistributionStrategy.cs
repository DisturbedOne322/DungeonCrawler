using Cysharp.Threading.Tasks;
using Data.UI;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using Gameplay.Player;
using Gameplay.Skills.Core;
using Gameplay.Units;
using PopupControllers;

namespace Gameplay.Rewards
{
    public class LootDistributionStrategy : BaseItemDistributionStrategy
    {
        private readonly EquipmentChangeController _equipmentChangeController;
        private readonly SkillDiscardController _skillDiscardController;
        private readonly PlayerSkillSlotsManager _skillSlotsManager;

        public LootDistributionStrategy(EquipmentChangeController equipmentChangeController,
            SkillDiscardController skillDiscardController, PlayerSkillSlotsManager skillSlotsManager, PlayerUnit playerUnit) : base(playerUnit)
        {
            _equipmentChangeController = equipmentChangeController;
            _skillDiscardController = skillDiscardController;
            _skillSlotsManager = skillSlotsManager;
        }

        protected override async UniTask HandleWeapon(BaseWeapon weapon)
        {
            if (Player.UnitEquipmentData.TryGetEquippedWeapon(out var currentWeapon))
            {
                var choice = await _equipmentChangeController.MakeEquipmentChoice(currentWeapon, weapon);
                if (choice == EquipmentSelectChoice.Change)
                    Player.UnitEquipmentData.EquipWeapon(weapon);
            }
            else
            {
                Player.UnitEquipmentData.EquipWeapon(weapon);
            }
        }

        protected override async UniTask HandleArmor(BaseArmor armor)
        {
            if (Player.UnitEquipmentData.TryGetEquippedArmor(out var currentArmor))
            {
                var choice = await _equipmentChangeController.MakeEquipmentChoice(currentArmor, armor);
                if (choice == EquipmentSelectChoice.Change)
                    Player.UnitEquipmentData.EquipArmor(armor);
            }
            else
            {
                Player.UnitEquipmentData.EquipArmor(armor);
            }
        }

        protected override async UniTask HandleSkill(BaseSkill skill)
        {
            if (_skillSlotsManager.HasFreeSkillSlot())
            {
                Player.UnitSkillsData.AddSkill(skill);
                return;
            }

            var skillToDiscard = await _skillDiscardController.MakeSkillDiscardChoice(skill, ItemObtainContext.Loot);
            if (skillToDiscard != skill)
            {
                Player.UnitSkillsData.RemoveSkill(skillToDiscard);
                Player.UnitSkillsData.AddSkill(skill);
            }
        }
    }
}