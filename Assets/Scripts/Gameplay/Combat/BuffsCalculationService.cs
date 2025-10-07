using System.Linq;
using Gameplay.Combat.Modifiers;

namespace Gameplay.Combat
{
    public class BuffsCalculationService
    {
        public int GetFinalOutgoingDamage(int baseDamage, in DamageContext damageContext)
        {
            int result = baseDamage;
            
            var modifiers = damageContext.Attacker.UnitActiveBuffsData.ActiveOffensiveBuffs;

            if (modifiers.Count > 0)
            {
                var additiveModifiers = modifiers.Where(x => x.PriorityType == OffensiveBuffPriorityType.Additive);
                var multiplicativeModifiers = modifiers.Where(x => x.PriorityType == OffensiveBuffPriorityType.Multiplicative);
                var stanceModifiers = modifiers.Where(x => x.PriorityType == OffensiveBuffPriorityType.Stance);

                foreach (var modifier in additiveModifiers) 
                    result = modifier.OffensiveBuffData.ModifyOutgoingDamage(result, damageContext);
            
                foreach (var modifier in multiplicativeModifiers) 
                    result = modifier.OffensiveBuffData.ModifyOutgoingDamage(result, damageContext);
                
                foreach (var modifier in stanceModifiers) 
                    result = modifier.OffensiveBuffData.ModifyOutgoingDamage(result, damageContext);
            }
            
            if (damageContext.IsCritical)
                result *= 2;
            
            return result;
        }
        
        public int GetReducedIngoingDamage(int incomingDamage, in DamageContext damageContext)
        {
            int reducedDamage = incomingDamage;
            
            var modifiers = damageContext.Defender.UnitActiveBuffsData.ActiveDefensiveBuffs;

            if (modifiers.Count > 0)
            {
                var subtractiveModifiers = modifiers.Where(x => x.PriorityType == DefensiveBuffPriorityType.Subtractive);
                var divisiveModifiers = modifiers.Where(x => x.PriorityType == DefensiveBuffPriorityType.Divisive);
                var stanceModifiers = modifiers.Where(x => x.PriorityType == DefensiveBuffPriorityType.Stance);
            
                foreach (var modifier in subtractiveModifiers) 
                    reducedDamage = modifier.DefensiveBuffData.ModifyIngoingDamage(reducedDamage, damageContext);
            
                foreach (var modifier in divisiveModifiers) 
                    reducedDamage = modifier.DefensiveBuffData.ModifyIngoingDamage(reducedDamage, damageContext);

                foreach (var modifier in stanceModifiers) 
                    reducedDamage = modifier.DefensiveBuffData.ModifyIngoingDamage(reducedDamage, damageContext);
            }
            
            return reducedDamage;
        }
    }
}