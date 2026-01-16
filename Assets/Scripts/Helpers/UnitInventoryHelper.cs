using System;
using Gameplay;
using Gameplay.Equipment.Armor;
using Gameplay.Equipment.Weapons;
using Gameplay.Facades;
using Gameplay.Skills.Core;
using Gameplay.StatusEffects.Core;

namespace Helpers
{
    public static class UnitInventoryHelper
    {
        public static bool HasItem(IGameUnit unit, BaseGameItem item)
        {
            switch (item)
            {
                case BaseSkill:
                    return HasSkill(unit, item);
                case BaseWeapon:
                    return HasWeapon(unit, item);
                case BaseArmor:
                    return HasArmor(unit, item);
                case BaseStatusEffectData:
                    return HasStatusEffect(unit, item);
            }

            throw new Exception($"UNHANDLED ITEM TYPE {item.GetType()}");
        }

        private static bool HasSkill(IGameUnit unit, BaseGameItem searchedSkill)
        {
            var unitSkills = unit.UnitSkillsContainer.Skills;

            for (var i = unitSkills.Count - 1; i >= 0; i--)
            {
                var skill = unitSkills[i];
                if (searchedSkill == skill)
                    return true;
            }

            if (searchedSkill == unit.UnitSkillsContainer.GuardSkill)
                return true;

            if (searchedSkill == unit.UnitSkillsContainer.BasicAttackSkill)
                return true;

            return false;
        }

        private static bool HasStatusEffect(IGameUnit unit, BaseGameItem searchedStatusEffect)
        {
            var unitStatusEffects = unit.UnitHeldStatusEffectsContainer.All;

            for (var i = unitStatusEffects.Count - 1; i >= 0; i--)
            {
                var heldEffect = unitStatusEffects[i];
                if (searchedStatusEffect == heldEffect.Effect)
                    return true;
            }

            return false;
        }

        private static bool HasWeapon(IGameUnit unit, BaseGameItem searchedWeapon)
        {
            if (!unit.UnitEquipmentData.TryGetEquippedWeapon(out var weapon))
                return false;

            return searchedWeapon == weapon;
        }

        private static bool HasArmor(IGameUnit unit, BaseGameItem searchedArmor)
        {
            if (!unit.UnitEquipmentData.TryGetEquippedArmor(out var armor))
                return false;

            return searchedArmor == armor;
        }
    }
}