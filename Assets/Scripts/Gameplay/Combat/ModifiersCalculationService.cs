using System.Collections.Generic;
using System.Linq;
using Gameplay.Combat.Data;
using Gameplay.Combat.Modifiers;
using Gameplay.Facades;

namespace Gameplay.Combat
{
    public class ModifiersCalculationService
    {
        public int GetFinalOutgoingDamage(in DamageContext damageContext)
        {
            int result = damageContext.SkillData.Damage;
            var modifiers = damageContext.Attacker.GetOffensiveModifiers();
            
            var additiveModifiers = modifiers.Where(x => x.Priority == OffensiveModifierPriorityType.Additive);
            var multiplicativeModifiers = modifiers.Where(x => x.Priority == OffensiveModifierPriorityType.Multiplicative);
            
            foreach (var modifier in additiveModifiers) 
                result = modifier.ModifyOutgoingDamage(result, damageContext);
            
            foreach (var modifier in multiplicativeModifiers) 
                result = modifier.ModifyOutgoingDamage(result, damageContext);
            
            result = ApplyCharge(result, damageContext);

            if (damageContext.IsCritical)
                result *= 2;
            
            return result;
        }
        
        public int GetReducedIngoingDamage(in DamageContext damageContext)
        {
            int result = damageContext.SkillData.Damage;
            var modifiers = damageContext.Defender.GetDefensiveModifiers();
            
            var subtractiveModifiers = modifiers.Where(x => x.Priority == DefensiveModifierPriorityType.Subtractive);
            var divisiveModifiers = modifiers.Where(x => x.Priority == DefensiveModifierPriorityType.Divisive);
            
            foreach (var modifier in subtractiveModifiers) 
                result = modifier.ModifyIngoingDamage(result, damageContext);
            
            foreach (var modifier in divisiveModifiers) 
                result = modifier.ModifyIngoingDamage(result, damageContext);
            
            return result;
        }
        
        private int ApplyCharge(int damage, in DamageContext damageContext)
        {
            var damageType = damageContext.SkillData.DamageType;
            var caster = damageContext.Attacker;
            bool consumeCharge = damageContext.ConsumeCharge;
            
            switch (damageType)
            {
                case DamageType.Physical:
                    return ProcessPhysicalAttack(caster, damage, consumeCharge);
                case DamageType.Magical:
                    return ProcessMagicalAttack(caster, damage, consumeCharge);
            }
            
            return damage;
        }

        private int ProcessPhysicalAttack(IEntity caster, int damage, bool consumeCharge)
        {
            if (caster.UnitBuffsData.Charged.Value)
            {
                if(consumeCharge)
                    caster.UnitBuffsData.Charged.Value = false;
                
                damage *= 2;
            }

            return damage;
        }
        
        private int ProcessMagicalAttack(IEntity caster, int damage, bool consumeCharge)
        {
            if (caster.UnitBuffsData.Concentrated.Value)
            {
                if(consumeCharge)
                    caster.UnitBuffsData.Concentrated.Value = false;
                
                damage *= 2;
            }

            return damage;
        }
    }
}