using Gameplay;
using Gameplay.Facades;

namespace Helpers
{
    public static class UnitInventoryHelper
    {
        public static bool HasSkill(IGameUnit unit, BaseGameItem searchedSkill)
        {
            var unitSkills = unit.UnitSkillsData.Skills;
            
            for (var i = unitSkills.Count - 1; i >= 0; i--)
            {
                var skill = unitSkills[i];
                if(searchedSkill == skill)
                    return true;
            }
            
            if(searchedSkill == unit.UnitSkillsData.GuardSkill)
                return true;
            
            if(searchedSkill == unit.UnitSkillsData.BasicAttackSkill)
                return true;
            
            return false;
        }
        
        public static bool HasStatusEffect(IGameUnit unit, BaseGameItem searchedStatusEffect)
        {
            var unitStatusEffects = unit.UnitHeldStatusEffectsData.All;
            
            for (var i = unitStatusEffects.Count - 1; i >= 0; i--)
            {
                var statusEffect = unitStatusEffects[i];
                if(searchedStatusEffect == statusEffect)
                    return true;
            }
            
            return false;
        }
        
        public static bool HasWeapon(IGameUnit unit, BaseGameItem searchedWeapon)
        {
            if(!unit.UnitEquipmentData.TryGetEquippedWeapon(out var weapon))
                return false;

            return searchedWeapon == weapon;
        }
        
        public static bool HasArmor(IGameUnit unit, BaseGameItem searchedArmor)
        {
            if(!unit.UnitEquipmentData.TryGetEquippedArmor(out var armor))
                return false;

            return searchedArmor == armor;
        }
    }
}