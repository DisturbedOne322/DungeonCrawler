using System.Collections.Generic;
using Gameplay.Buffs.Core;
using Gameplay.Buffs.DefensiveCore;
using Gameplay.Buffs.OffensiveCore;

namespace Gameplay.Buffs.Services
{
    public class BuffsCalculationService
    {
        private const int DefaultBuffsCapacity = 20;
        
        private readonly List<OffensiveBuffInstance> _additiveBuffs = new(DefaultBuffsCapacity);
        private readonly List<OffensiveBuffInstance> _multiplicativeBuffs = new(DefaultBuffsCapacity);
        private readonly List<OffensiveBuffInstance> _offensiveStanceBuffs = new(DefaultBuffsCapacity);
        
        private readonly List<DefensiveBuffInstance> _subtractiveBuffs = new(DefaultBuffsCapacity);
        private readonly List<DefensiveBuffInstance> _divisiveBuffs = new(DefaultBuffsCapacity);
        private readonly List<DefensiveBuffInstance> _defensiveStanceBuffs = new(DefaultBuffsCapacity);
        
        public int GetFinalOutgoingDamage(int baseDamage, in DamageContext damageContext)
        {
            int result = baseDamage;
            
            var buffs = damageContext.Attacker.UnitActiveBuffsData.ActiveOffensiveBuffs;

            int count = buffs.Count;
            
            if (count > 0 && damageContext.SkillData.CanBeBuffed) 
                result = ApplyOffensiveBuffs(damageContext, buffs, result);
            
            if (damageContext.IsCritical)
                result *= 2;
            
            return result;
        }

        public int GetReducedIngoingDamage(int incomingDamage, in DamageContext damageContext)
        {
            int reducedDamage = incomingDamage;
            
            var buffs = damageContext.Defender.UnitActiveBuffsData.ActiveDefensiveBuffs;

            if (buffs.Count > 0) 
                reducedDamage = ApplyDefensiveBuffs(damageContext, buffs, reducedDamage);
            
            return reducedDamage;
        }

        private int ApplyOffensiveBuffs(DamageContext damageContext, List<OffensiveBuffInstance> buffs, int result)
        {
            _additiveBuffs.Clear();
            _multiplicativeBuffs.Clear();
            _offensiveStanceBuffs.Clear();
            
            foreach (var buff in buffs)
            {
                switch (buff.PriorityType)
                {
                    case OffensiveBuffPriorityType.Additive:
                        _additiveBuffs.Add(buff);
                        break;
                    case OffensiveBuffPriorityType.Multiplicative:
                        _multiplicativeBuffs.Add(buff);
                        break;
                    case OffensiveBuffPriorityType.Stance:
                        _offensiveStanceBuffs.Add(buff);
                        break;
                }
            }
            
            foreach (var buff in _additiveBuffs)
                result = buff.OffensiveBuffData.ModifyOutgoingDamage(result, damageContext);

            foreach (var buff in _multiplicativeBuffs)
                result = buff.OffensiveBuffData.ModifyOutgoingDamage(result, damageContext);

            foreach (var buff in _offensiveStanceBuffs)
                result = buff.OffensiveBuffData.ModifyOutgoingDamage(result, damageContext);
            
            return result;
        }
        
        private int ApplyDefensiveBuffs(DamageContext damageContext, List<DefensiveBuffInstance> buffs, int result)
        {
            _subtractiveBuffs.Clear(); 
            _divisiveBuffs.Clear();
            _defensiveStanceBuffs.Clear();

            foreach (var buff in buffs)
            {
                switch (buff.PriorityType)
                {
                    case DefensiveBuffPriorityType.Subtractive:
                        _subtractiveBuffs.Add(buff);
                        break;
                    case DefensiveBuffPriorityType.Divisive:
                        _divisiveBuffs.Add(buff);
                        break;
                    case DefensiveBuffPriorityType.Stance:
                        _defensiveStanceBuffs.Add(buff);
                        break;
                }
            }
            
            foreach (var buff in _subtractiveBuffs)
                result = buff.DefensiveBuffData.ModifyIngoingDamage(result, damageContext);

            foreach (var buff in _divisiveBuffs)
                result = buff.DefensiveBuffData.ModifyIngoingDamage(result, damageContext);

            foreach (var buff in _defensiveStanceBuffs)
                result = buff.DefensiveBuffData.ModifyIngoingDamage(result, damageContext);
            
            return result;
        }
    }
}