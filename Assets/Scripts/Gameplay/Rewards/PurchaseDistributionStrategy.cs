using AssetManagement.AssetProviders.ConfigProviders;
using Cysharp.Threading.Tasks;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using Gameplay.Player;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Buffs.Services;
using Gameplay.Units;
using PopupControllers;

namespace Gameplay.Rewards
{
    public class PurchaseDistributionStrategy : BaseItemDistributionStrategy
    {
        private readonly SkillDiscardController _skillDiscardController;
        private readonly PlayerSkillSlotsManager _skillSlotsManager;

        public PurchaseDistributionStrategy(SkillDiscardController skillDiscardController,
            PlayerSkillSlotsManager skillSlotsManager, PlayerUnit playerUnit,
            GameplayConfigsProvider configsProvider,
            StatusEffectsProcessor statusEffectsProcessor) : base(playerUnit, configsProvider, statusEffectsProcessor)
        {
            _skillDiscardController = skillDiscardController;
            _skillSlotsManager = skillSlotsManager;
        }

        protected override UniTask HandleWeapon(BaseWeapon weapon)
        {
            Player.UnitEquipmentData.EquipWeapon(weapon);
            return UniTask.CompletedTask;
        }

        protected override UniTask HandleArmor(BaseArmor armor)
        {
            Player.UnitEquipmentData.EquipArmor(armor);
            return UniTask.CompletedTask;
        }

        protected override async UniTask HandleSkill(BaseSkill skill)
        {
            if (_skillSlotsManager.HasFreeSkillSlot())
            {
                Player.UnitSkillsData.AddSkill(skill);
                return;
            }

            var skillToDiscard =
                await _skillDiscardController.MakeSkillDiscardChoice(skill, ItemObtainContext.Purchase);

            if (skillToDiscard != skill)
            {
                Player.UnitSkillsData.RemoveSkill(skillToDiscard);
                Player.UnitSkillsData.AddSkill(skill);
            }
        }
    }
}